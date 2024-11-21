using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            return _esContext.CarritoCompra.Include(c => c.BicisCantidad).ToList();
        }

        // mirar carrito
        [HttpGet("idDelCarrito")]
        public CarritoCompra GetCarrito(int idCarrito)
        { 
            return _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.Id == idCarrito);
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
                    if (item.bicicletas.Id == idBicicleta && encontrada == false)
                    {
                        item.cantidad++;
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
        /*
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

        // dar la cantidad de los carritos
        [HttpGet("cantidadBicis")]
        public IList<BicisCantidad> GetCarritoCantidad(int idCarrito)
        {
            CarritoCompra carritoCompra = _esContext.CarritoCompra.Include(c => c.Bicicletas).FirstOrDefault(r => r.Id == idCarrito);
            IList<Bicicletas> bicis = carritoCompra.Bicicletas;
            IList<BicisCantidad> bicisCantidad = new List<BicisCantidad>();
            Boolean nuevaBici = false;
            foreach (var item in bicis)
            {
                foreach (var item1 in bicisCantidad)
                {
                    if (item1.idBici == item.Id)
                    {
                        item1.cantidad++;
                    }
                    else
                    {
                        nuevaBici = true;
                    }
                }
                if (nuevaBici || bicisCantidad.IsNullOrEmpty())
                {
                    BicisCantidad biciTmp = new BicisCantidad()
                    {
                        idBici = item.Id,
                        cantidad = 1
                    };

                    bicisCantidad.Add(biciTmp);
                }

            }
            return bicisCantidad;
        }
        */
    }
}
