using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        [HttpGet("/AdminView")]
        public IEnumerable<UserToAdmin> GetUsuariosAdmin()
        {
            IEnumerable<Usuarios> usuarios = _esContext.Usuarios.ToList();

            IEnumerable<UserToAdmin> userDto = usuarios.Select(u => new UserToAdmin
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Direccion = u.Direccion,
                Admin = u.Admin,
            });

            return userDto;
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

        [HttpDelete("/deleteUsuarioId")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            Usuarios user = Uuarios(id);

            if (user == null)
            {
                return NotFound("No se encuentra usuario");
            }

            _esContext.Remove(user);
            await _esContext.SaveChangesAsync();

            return Ok("Usuario eliminado");
        }

        [HttpPost("/updateUsuario")]
        public async Task<ActionResult> UptadteUser(UserToAdmin userIndicado)
        {
            if (userIndicado == null)
            {
                return BadRequest("Usuario no indicado");
            }

            Usuarios user = Uuarios(userIndicado.Id);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            user.Name = userIndicado.Name;
            user.Email = userIndicado.Email;
            user.Direccion = userIndicado.Direccion;
            user.Admin = userIndicado.Admin;

            await _esContext.SaveChangesAsync();
            return Ok("usuario editado");

        }

        [HttpPost("/updateUsuarioPasword")]
        public async Task<ActionResult> UptadteUserPasword(int id, string password)
        {

            Usuarios user = Uuarios(id);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            user.Password = PasswordHelper.Hash(password);

            await _esContext.SaveChangesAsync();
            return Ok("Contraseña cambiada");

        }
    }
}
