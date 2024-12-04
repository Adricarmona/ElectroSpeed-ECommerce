using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ElectroSpeedContext _esContext;

        public UserController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        [HttpGet]
        public IEnumerable<Usuarios> GetUsuarios()
        {
            return _esContext.Usuarios.Include(c => c.carritos).ToList();
        }

        [HttpGet("/usuarioId")]
        public Usuarios Uuarios(int id)
        {
            return _esContext.Usuarios.Include(c => c.carritos).FirstOrDefault(r => r.Id == id);
        }

        [HttpGet("/usuarioEmail")]
        public Usuarios UsuariosEmail(string email)
        {
            return _esContext.Usuarios.Include(c => c.carritos).FirstOrDefault(r => r.Email == email);
        }



        [HttpGet("/usuario/detalle")]
        [Authorize]
        public ActionResult GetUsuarioDetalle()
        {
            int userId = Int32.Parse(User.FindFirst("id").Value); // Obtener ID del usuario desde el token

            // Obtener el usuario con sus carritos y orden temporal
            var usuario = _esContext.Usuarios
                .Include(u => u.carritos)
                    .ThenInclude(c => c.BicisCantidad)
                    .ThenInclude(bc => bc.bicicletas)
                .Include(u => u.ordenTemporal) // Si 'ordenTemporal' tiene información relevante
                .FirstOrDefault(u => u.Id == userId);

            // Verificar si el usuario existe
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            // Obtener los pedidos del usuario
            var pedidos = _esContext.Pedidos
                .Where(p => p.UsuarioId == userId) // Filtrar por el ID del usuario
                .OrderByDescending(p => p.Fecha) // Ordenar por fecha de pedido (suponiendo que tienes un campo 'Fecha')
                .Select(p => new
                {
                    Fecha = p.Fecha,
                    Productos = p.BicisCantidad.Select(b => new
                    {
                        b.bicicletas.MarcaModelo,
                        b.bicicletas.UrlImg,
                        b.cantidad,
                        PrecioUnitario = b.bicicletas.Precio
                    }),
                    PrecioTotal = p.BicisCantidad.Sum(b => b.cantidad * b.bicicletas.Precio),
                    MetodoPago = p.MetodoPago // Cambia esto según cómo se almacena el método de pago en la entidad 'Pedidos'
                }).ToList();

            // Retornar la información del usuario junto con sus pedidos
            return Ok(new
            {
                usuario.Name,
                usuario.Email,
                usuario.Direccion,
                Rol = usuario.Name == "Riki" || usuario.Name == "Adri" || usuario.Name == "Hector" || usuario.Name == "Noe" ? "Admin" : "Cliente",
                Pedidos = pedidos
            });
        }


        [HttpPut("/usuario/actualizar")]
        [Authorize]
        public ActionResult UpdateUsuario([FromBody] UpdateUsuario model)
        {
            int userId = Int32.Parse(User.FindFirst("id").Value);
            var usuario = _esContext.Usuarios.FirstOrDefault(u => u.Id == userId);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            usuario.Name = model.Name ?? usuario.Name;
            usuario.Email = model.Email ?? usuario.Email;
            usuario.Direccion = model.Direccion ?? usuario.Direccion;
            if (!string.IsNullOrWhiteSpace(model.Password))
                usuario.Password = PasswordHelper.Hash(model.Password);

            _esContext.SaveChanges();
            return Ok("Información actualizada correctamente");
        }
    }
}
