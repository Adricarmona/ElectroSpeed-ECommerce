using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace ElectroSpeed_server.Controllers
{
    [Route("[controller]")]
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
            return _esContext.CarritoCompra.Include(c => c.BicisCantidad).ToList();
        }

        // mirar carrito
        [HttpGet]
        public CarritoCompra GetCarritoUserId(int idusuario)
        { 
            return _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.UsuarioId == idusuario);
        }

        // mirar carrito
        [HttpGet("idDelUsuario")]
        public CarritoCompra GetIdCarrito(int idUsuario)
        {
            return _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.UsuarioId == idUsuario);
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

            IList<BicisCantidad> biciCantidadActual = _esContext.BiciCantidad.ToList();

            Bicicletas bicicleta = await _esContext.Bicicletas.FirstOrDefaultAsync(b => b.Id == idBicicleta);

            if (bicicleta == null)
            {
                return NotFound("Bicicleta no encontrada.");
            }

            Boolean encontrada = false; // si esta encontrada la bici
            foreach (var item in biciCantidadActual)
            {
                if (item.idCarrito == carritoId)
                {
                    if (item.IdBici == idBicicleta && encontrada == false)
                    {
                        item.cantidad++;
                        encontrada = true;
                    }
                }
            }

            if (encontrada == false)
            {
                BicisCantidad biciTmp = new BicisCantidad()
                {
                    IdBici = bicicleta.Id,
                    idCarrito = carritoId,
                    cantidad = 1,
                };

                carritoActual.BicisCantidad.Add(biciTmp);
            }
            _esContext.SaveChanges();

            return Ok("bicicleta añadida al carrito");
        }
        
        // Quitar producto del carrito
        [HttpDelete("{carritoId}")]
        public async Task<ActionResult> DeleteItem(int carritoId, int bicicletaId)
        {
            var carrito = GetCarritoUserId(carritoId);

            if (carrito == null)
            {
                return NotFound("no se encuentra ese carrito");
            }

            IList<BicisCantidad> bicisCantidad = carrito.BicisCantidad;

            if (bicisCantidad.IsNullOrEmpty())
            {
                return Ok("Carrito vacio");
            }

            BicisCantidad bicisTMP = new BicisCantidad();
            Boolean eliminar = false;
            foreach (var item in bicisCantidad)
            {
                if(item.IdBici == bicicletaId && eliminar == false)
                { 
                    if (item.cantidad > 1)
                    {
                        item.cantidad--;
                    } else
                    {
                        bicisTMP = item;
                        eliminar = true;
                        
                    }
                }
            }

            carrito.BicisCantidad.Remove(bicisTMP);

            await _esContext.SaveChangesAsync();

            return Ok("eliminado del carrito");
        }
    }
}
