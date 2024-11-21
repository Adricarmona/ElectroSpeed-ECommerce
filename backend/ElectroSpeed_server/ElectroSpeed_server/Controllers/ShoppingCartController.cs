using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ElectroSpeedContext _esContext;

        public ShoppingCartController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        // mirar todos carritos
        [HttpGet("carritos")]
        public List<CarritoCompra> GetAllCarrito()
        {
            return _esContext.CarritoCompra.ToList();
        }

        // mirar carrito
        [HttpGet("idDelCarrito")]
        public CarritoCompra GetCarrito(int idCarrito)
        { 
            return _esContext.CarritoCompra.FirstOrDefault(r => r.Id == idCarrito);
        }

        // mirar carrito por id de usuario
        [HttpGet("idDelUsuario")]
        public CarritoCompra GetCarritoPorUsuario(int idUsuario)
        {
            return _esContext.CarritoCompra.FirstOrDefault(r => r.UsuarioId == idUsuario);
        }

        // añadir productos
        [HttpPut("addProduct")]
        public async Task<ActionResult> addProduct(int carritoId, int idBicicleta)
        {
            var carritoActual = await _esContext.CarritoCompra.Include(c => c.Bicicletas).FirstOrDefaultAsync(c => c.Id == carritoId);

            if (carritoActual == null) {
                return NotFound("no encontrado");
            }

            var bicicleta = await _esContext.Bicicletas.FirstOrDefaultAsync(b => b.Id == idBicicleta);

            if (bicicleta == null)
            {
                return NotFound("Bicicleta no encontrada.");
            }

            carritoActual.Bicicletas.Add(bicicleta);
            await _esContext.SaveChangesAsync();

            return Ok("bicicleta añadida al carrito");
        }

        // Quitar producto del carrito
        [HttpDelete("{carritoId}")]
        public async Task<ActionResult> DeleteItem(int carritoId)
        {
            var item = await _esContext.CarritoCompra.FindAsync(carritoId);

            if (item == null)
            {
                return NotFound("no se encuentra ese producto en el carrito");
            }

            _esContext.CarritoCompra.Remove(item);
            await _esContext.SaveChangesAsync();

            return Ok("eliminado del carrito");
        }

        // Reserva del stock y redirigir al pago
        /*
        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout(int? userId, string metodoPago)
        {
            var carrito = await _esContext.CarritoCompra
                .Include(c => c.Bicletas)
                .Where(c => c.UsuariosId == userId)
                .ToListAsync();

            if (!carrito.Any())
            {
                return BadRequest("carrito vacio");
            }

            foreach (var item in carrito)
            {
                if (item.Bicletas.Stock < 1)
                {
                    return BadRequest($"{item.Bicletas.MarcaModelo} no tiene stock");
                }

                item.Bicletas.Stock--;
            }

            await _esContext.SaveChangesAsync();

            // Redirigir al metodo de pago con parametros
            return Redirect($"/checkout?metodoPago={metodoPago}");
        }
        */
    }
}
