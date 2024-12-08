using ElectroSpeed_server.Models.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class BicisCantidadRepository : esRepository<BicisCantidad>
    {
        private readonly ElectroSpeedContext _context;

        public BicisCantidadRepository(ElectroSpeedContext context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BicisCantidad>> GetAllBicisCantidadAsync()
        {
            return await _context.BiciCantidad.Include(b => b.bicicletas).Include(b => b.CarritoCompra).ToListAsync();
        }

        public async Task<BicisCantidad> GetBicisCantidadByIdAsync(int id)
        {
            return await _context.BiciCantidad.Include(b => b.bicicletas).Include(b => b.CarritoCompra).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBicisCantidadAsync(BicisCantidad biciCantidad)
        {
            await _context.BiciCantidad.AddAsync(biciCantidad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBicisCantidadAsync(BicisCantidad biciCantidad)
        {
            _context.BiciCantidad.Update(biciCantidad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBicisCantidadAsync(int id)
        {
            var biciCantidad = await _context.BiciCantidad.FindAsync(id);
            if (biciCantidad != null)
            {
                _context.BiciCantidad.Remove(biciCantidad);
                await _context.SaveChangesAsync();
            }
        }
    }

}
