using Core.Infrastructure.DAL;
using Core.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public abstract class BaseDAL<TEntity> : IDataAccessLayer<TEntity>
        where TEntity : class, IEntityWithId
    {
        private readonly DbContext _dbContext;
        public BaseDAL(DbContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task AddEntityAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}