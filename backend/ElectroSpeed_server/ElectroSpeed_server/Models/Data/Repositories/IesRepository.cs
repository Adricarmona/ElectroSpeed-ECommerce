﻿namespace ElectroSpeed_server.Models.Data.Repositories
{
    public interface IesRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueryable(bool asNoTracking = true);
        Task <TEntity> GetByIdAsync(object id);
        Task <TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> SaveAsync();
        Task<bool> ExistAsync(object id);
    }
}
