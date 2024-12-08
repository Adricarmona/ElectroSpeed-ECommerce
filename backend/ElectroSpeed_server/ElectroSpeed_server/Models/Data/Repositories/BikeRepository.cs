using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class BikeRepository : esRepository<Bicicletas>
    {
        private readonly ElectroSpeedContext _context;

        public BikeRepository(ElectroSpeedContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bicicletas>> GetAllBikesAsync()
        {
            return await _context.Bicicletas.ToListAsync();
        }

        public async Task<Bicicletas> GetBikeByIdAsync(int id)
        {
            return await _context.Bicicletas.FindAsync(id);
        }

        public async Task AddBikeAsync(Bicicletas bike)
        {
            await _context.Bicicletas.AddAsync(bike);
        }

        public async Task UpdateBikeAsync(Bicicletas bike)
        {
            _context.Bicicletas.Update(bike);
        }

        public async Task DeleteBikeAsync(int id)
        {
            var bike = await _context.Bicicletas.FindAsync(id);
            if (bike != null)
            {
                _context.Bicicletas.Remove(bike);
            }
        }
    }
}
