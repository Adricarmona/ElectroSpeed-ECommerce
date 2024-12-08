using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroSpeed_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // Obtener todos los carritos
        [HttpGet("carritos")]
        public async Task<ActionResult<List<CarritoCompra>>> GetAllCarrito()
        {
            var carritos = await _shoppingCartService.GetAllCarritos();
            return Ok(carritos);
        }

        // Obtener carrito por id de usuario
        [HttpGet("idDelUsuario")]
        public async Task<ActionResult<CarritoCompra>> GetCarritoUserId(int idusuario)
        {
            var carrito = await _shoppingCartService.GetCarritoByUserId(idusuario);

            if (carrito == null)
            {
                return NotFound("Carrito no encontrado.");
            }

            return Ok(carrito);
        }

        // Obtener carrito por id
        [HttpGet("{carritoId}")]
        public async Task<ActionResult<CarritoCompra>> GetIdCarrito(int carritoId)
        {
            var carrito = await _shoppingCartService.GetCarritoByUserId(carritoId);

            if (carrito == null)
            {
                return NotFound("Carrito no encontrado.");
            }

            return Ok(carrito);
        }

        // Añadir producto al carrito
        [HttpPut("addProduct")]
        public async Task<ActionResult> AddProduct(int carritoId, int idBicicleta)
        {
            var bici = await _shoppingCartService.AddBiciToCarrito(carritoId, idBicicleta);

            if (bici == null)
            {
                return NotFound("Bicicleta o carrito no encontrado.");
            }

            return Ok("Bicicleta añadida al carrito.");
        }

        // Quitar producto del carrito
        [HttpDelete("{carritoId}")]
        public async Task<ActionResult> DeleteItem(int carritoId, int bicicletaId)
        {
            var bici = await _shoppingCartService.RemoveBiciFromCarrito(carritoId, bicicletaId);

            if (bici == null)
            {
                return NotFound("Carrito o bicicleta no encontrado.");
            }

            return Ok("Bicicleta eliminada del carrito.");
        }

        // Quitar todos los productos de un carrito
        [HttpDelete("clear/{carritoId}")]
        public async Task<ActionResult> ClearCarrito(int carritoId)
        {
            await _shoppingCartService.ClearCarrito(carritoId);
            return Ok("Todos los productos fueron eliminados del carrito.");
        }

    }
}

