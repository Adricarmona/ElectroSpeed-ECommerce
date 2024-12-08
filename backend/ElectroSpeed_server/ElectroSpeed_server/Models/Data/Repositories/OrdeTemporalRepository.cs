using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class OrdeTemporalRepository : esRepository<OrdeTemporal>
    {
        private readonly ElectroSpeedContext _context;

        public OrdeTemporalRepository(ElectroSpeedContext context) :base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdeTemporal>> GetAllOrdersAsync()
        {
            return await _context.OrdeTemporal.Include(o => o.BicisCantidad).ToListAsync();
        }

        public async Task<OrdeTemporal> GetOrderByIdAsync(int id)
        {
            return await _context.OrdeTemporal.Include(o => o.BicisCantidad).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrderAsync(OrdeTemporal order)
        {
            await _context.OrdeTemporal.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(OrdeTemporal order)
        {
            _context.OrdeTemporal.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.OrdeTemporal.FindAsync(id);
            if (order != null)
            {
                _context.OrdeTemporal.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }

}
