using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Obtiene los prodductos del carrito del usuario
        [HttpGet("{userId?}")]
        public async Task<ActionResult> GetCart(int? userId)
        {
            var carrito = userId == null
                ? await _esContext.CarritoCompra
                    .Include(c => c.Bicletas)
                    .Where(c => c.UsuariosId == null)
                    .ToListAsync()
                : await _esContext.CarritoCompra
                    .Include(c => c.Bicletas)
                    .Where(c => c.UsuariosId == userId)
                    .ToListAsync();

            if (!carrito.Any())
            {
                return NotFound("carito vacio");
            }

            var response = carrito.Select(item => new
            {
                item.Id,
                Producto = item.Bicletas.MarcaModelo,
                Imagen = item.Bicletas.UrlImg,
                Precio = item.Bicletas.Precio,
                Cantidad = item.Bicletas.Stock > 0 ? item.Bicletas.Stock : 0
            });

            return Ok(response);
        }

        // Cambiar la cantidad del producto en el carrito
        [HttpPut("updateQuantity")]
        public async Task<ActionResult> UpdateQuantity(int carritoId, int nuevaCantidad)
        {
            var item = await _esContext.CarritoCompra
                .Include(c => c.Bicletas)
                .FirstOrDefaultAsync(c => c.Id == carritoId);

            if (item == null)
            {
                return NotFound("no se encuentra ese producto en el carrito");
            }

            if (nuevaCantidad > item.Bicletas.Stock)
            {
                return BadRequest($"No hay stock. Hay {item.Bicletas.Stock} unidades disponibles");
            }

            item.Bicletas.Stock -= (nuevaCantidad - item.Bicletas.Stock);
            await _esContext.SaveChangesAsync();

            return Ok("Cantidad actualizada");
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
    }
}
