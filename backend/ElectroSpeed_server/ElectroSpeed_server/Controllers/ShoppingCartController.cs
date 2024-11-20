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

        // Obtiene los productos del carrito del usuario
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

        // mirar carrito
        [HttpGet("idDelCarrito")]
        public CarritoCompra GetCarrito(int idCarrito)
        { 
            return _esContext.CarritoCompra.FirstOrDefault(r => r.Id == idCarrito);
        }

        // Añadir un producto al carrito
        [HttpPost("addToCart")]
        public async Task<ActionResult> AddToCart(int userId, int productoId, int cantidad)
        {
            // busca el producto en la base de datos
            var producto = await _esContext.Bicicletas.FirstOrDefaultAsync(b => b.Id == productoId);
            if (producto == null)
            {
                return NotFound("el producto no existe");
            }

            // co si hay stock
            if (cantidad > producto.Stock)
            {
                return BadRequest($"no hay suficiente stock, el stock es: {producto.Stock}");
            }

            // mira si el producto ya esta en el carrito
            var itemCarrito = await _esContext.CarritoCompra
                .FirstOrDefaultAsync(c => c.UsuariosId == userId && c.BicicletasId == productoId);

            if (itemCarrito != null)
            {
                // si ya esta en el carro, actualiza cantidad
                if (itemCarrito.Bicletas.Stock < cantidad)
                {
                    return BadRequest($"Se pasa del stock disponible");
                }

                itemCarrito.Bicletas.Stock -= cantidad;
            }
            else
            {
                // si no está, lo añade
                var nuevoItem = new CarritoCompra
                {
                    BicicletasId = productoId,
                    UsuariosId = userId,
                };

                _esContext.CarritoCompra.Add(nuevoItem);
            }

            // actualiza el stock del pruducto
            producto.Stock -= cantidad;

            await _esContext.SaveChangesAsync();

            return Ok("producto añadido al carrito");
        }

        // cambiar cantidad del producto
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

            // verifica stock
            if (nuevaCantidad > item.Bicletas.Stock)
            {
                return BadRequest($"no hay suficiente stock, el stock es: {item.Bicletas.Stock}");
            }

            if (nuevaCantidad < 1)
            {
                return BadRequest("el minimo es 1");
            }

            // calcula diferencia de cantidad y actualiza stock
            var diferencia = nuevaCantidad - item.Bicletas.Stock;
            item.Bicletas.Stock -= diferencia;

            await _esContext.SaveChangesAsync();

            return Ok("cantidad actualizada");
        }

        // eliminar del carrito
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

        // reserva del stock y redirijir pago
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

            // redirigir al pago con parametros
            return Redirect($"/checkout?metodoPago={metodoPago}");
        }
        */
    }
}
