using Core.Infrastructure.Models;

namespace Core.Infrastructure.DAL
{
    public interface IDataAccessLayer<TEntity> : ICanAddEntity<TEntity>, ICanSaveChanges
        where TEntity : class, IEntityWithId
    {
    }
}
