using Core.Infrastructure.Models;

namespace Core.Infrastructure
{
    public interface ICanAddEntity<in TEntity>
        where TEntity : class, IEntityWithId
    {
        public Task AddEntityAsync(TEntity entity);
    }
}
