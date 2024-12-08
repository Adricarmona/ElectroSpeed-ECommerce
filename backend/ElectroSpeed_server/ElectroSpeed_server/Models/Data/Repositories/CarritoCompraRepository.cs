using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class CarritoCompraRepository : esRepository<CarritoCompra>
    {
        private readonly ElectroSpeedContext _context;

        public CarritoCompraRepository(ElectroSpeedContext context) :base(context) 
        {
            _context = context;
        }

        public async Task<List<CarritoCompra>> GetAllCarritos()
        {
            return await _context.CarritoCompra.Include(c => c.BicisCantidad).ToListAsync();
        }

        public async Task<CarritoCompra> GetCarritoByUserId(int userId)
        {
            return await _context.CarritoCompra.Include(c => c.BicisCantidad)
                                               .FirstOrDefaultAsync(r => r.UsuarioId == userId);
        }

        public async Task<BicisCantidad> AddBiciToCarrito(int carritoId, int biciId)
        {
            var carrito = await _context.CarritoCompra.Include(c => c.BicisCantidad)
                                                       .FirstOrDefaultAsync(c => c.Id == carritoId);

            if (carrito == null)
                return null;

            var biciCantidad = await _context.BiciCantidad
                                              .FirstOrDefaultAsync(b => b.idCarrito == carritoId && b.IdBici == biciId);

            if (biciCantidad != null)
            {
                biciCantidad.cantidad++;
            }
            else
            {
                var biciTmp = new BicisCantidad()
                {
                    IdBici = biciId,
                    idCarrito = carritoId,
                    cantidad = 1
                };

                carrito.BicisCantidad.Add(biciTmp);
            }

            await _context.SaveChangesAsync();
            return biciCantidad;
        }

        public async Task<BicisCantidad> RemoveBiciFromCarrito(int carritoId, int biciId)
        {
            var carrito = await _context.CarritoCompra.Include(c => c.BicisCantidad)
                                                       .FirstOrDefaultAsync(c => c.Id == carritoId);

            if (carrito == null)
                return null;

            var biciCantidad = carrito.BicisCantidad.FirstOrDefault(b => b.IdBici == biciId);

            if (biciCantidad != null)
            {
                if (biciCantidad.cantidad > 1)
                {
                    biciCantidad.cantidad--;
                }
                else
                {
                    carrito.BicisCantidad.Remove(biciCantidad);
                }

                await _context.SaveChangesAsync();
            }

            return biciCantidad;
        }
    }
}
