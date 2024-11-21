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
            return _esContext.CarritoCompra.Include(c => c.Bicicletas).ToList();
        }

        // mirar carrito
        [HttpGet("idDelCarrito")]
        public CarritoCompra GetCarrito(int idCarrito)
        { 
            return _esContext.CarritoCompra.Include(c => c.Bicicletas).FirstOrDefault(r => r.Id == idCarrito);
        }

        // mirar carrito
        [HttpGet("idDelUsuario")]
        public CarritoCompra GetIdCarrito(int idUsuario)
        {
            return _esContext.CarritoCompra.Include(c => c.Bicicletas).FirstOrDefault(r => r.UsuarioId == idUsuario);
        }

        // añadir productos
        [HttpPut("addProduct")]
        public async Task<ActionResult> addProduct(int carritoId, int idBicicleta)
        {
            CarritoCompra carritoActual = await _esContext.CarritoCompra.FirstOrDefaultAsync(c => c.Id == carritoId);

            if (carritoActual == null) 
            {
                return NotFound("no encontrado");
            }

            Bicicletas bicicleta = await _esContext.Bicicletas.FirstOrDefaultAsync(b => b.Id == idBicicleta);

            if (bicicleta == null)
            {
                return NotFound("Bicicleta no encontrada.");
            }

            carritoActual.Bicicletas.Add(bicicleta);
            _esContext.SaveChanges();

            return Ok("bicicleta añadida al carrito");
        }

        // Quitar producto del carrito
        [HttpDelete("{carritoId}")]
        public async Task<ActionResult> DeleteItem(int carritoId, int bicicletaId)
        {
            var carrito = GetCarrito(carritoId);

            if (carrito == null)
            {
                return NotFound("no se encuentra ese carrito");
            }

            Bicicletas bicicleta = carrito.Bicicletas.FirstOrDefault(b => b.Id == bicicletaId);

            if (bicicleta == null)
            {
                return NotFound("no se encuentra esa bici");
            }

            carrito.Bicicletas.Remove(bicicleta);
            await _esContext.SaveChangesAsync();

            return Ok("eliminado del carrito");
        }
    }
}
