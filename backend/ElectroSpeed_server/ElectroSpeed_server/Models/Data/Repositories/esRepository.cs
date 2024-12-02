using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public abstract class esRepository <TEntity> : IesRepository<TEntity> where TEntity : class
    {
        private readonly ElectroSpeedContext _context;

        public esRepository(ElectroSpeedContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToArrayAsync();
        }

        public IQueryable<TEntity> GetQueryable(bool asNoTracking = true)
        {
            DbSet<TEntity> entities = _context.Set<TEntity>();
            return asNoTracking ? entities.AsNoTracking() : entities;
        }


        //Enviar bicicleta por id
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            EntityEntry<TEntity> entry = await _context.Set<TEntity>().AddAsync(entity);
            await SaveAsync();

            return entry.Entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Set<TEntity>().Update(entity);
            await SaveAsync();

            return entry.Entity;
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> ExistAsync(object id)
        {
            return await GetByIdAsync(id) != null;
        }
    }
}
