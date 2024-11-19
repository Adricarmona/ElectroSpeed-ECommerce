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

        // Obtiene los prodductos de la ccarrito del ususario
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

            if (!carrito.Any()) return NotFound("El carrito está vacío.");

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

        // cambiar la cantidad del producto en el carrito
        [HttpPut("updateQuantity")]
        public async Task<ActionResult> UpdateQuantity(int carritoId, int nuevaCantidad)
        {
            var item = await _esContext.CarritoCompra
                .Include(c => c.Bicletas)
                .FirstOrDefaultAsync(c => c.Id == carritoId);

            if (item == null) return NotFound("Producto no encontrado en el carrito.");
            if (nuevaCantidad > item.Bicletas.Stock)
                return BadRequest($"Stock insuficiente. Solo hay {item.Bicletas.Stock} unidades disponibles.");

            item.Bicletas.Stock -= (nuevaCantidad - item.Bicletas.Stock);
            await _esContext.SaveChangesAsync();

            return Ok("Cantidad actualizada correctamente.");
        }

        // quitar producto del carrito
        [HttpDelete("{carritoId}")]
        public async Task<ActionResult> DeleteItem(int carritoId)
        {
            var item = await _esContext.CarritoCompra.FindAsync(carritoId);
            if (item == null) return NotFound("Producto no encontrado en el carrito.");

            _esContext.CarritoCompra.Remove(item);
            await _esContext.SaveChangesAsync();

            return Ok("Producto eliminado del carrito.");
        }

        // reserva del stock y redirigir al pago
        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout(int? userId, string metodoPago)
        {
            var carrito = await _esContext.CarritoCompra
                .Include(c => c.Bicletas)
                .Where(c => c.UsuariosId == userId)
                .ToListAsync();

            if (!carrito.Any()) return BadRequest("El carrito está vacío.");

            foreach (var item in carrito)
            {
                if (item.Bicletas.Stock < 1)
                {
                    return BadRequest($"El producto {item.Bicletas.MarcaModelo} no tiene stock suficiente.");
                }

                item.Bicletas.Stock--;
            }

            await _esContext.SaveChangesAsync();

            // redirigir al metodo de pago con parametros
            return Redirect($"/checkout?metodoPago={metodoPago}");
        }
    }
}
