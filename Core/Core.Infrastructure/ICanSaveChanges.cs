namespace Core.Infrastructure
{
    public interface ICanSaveChanges
    {
        public Task SaveChangesAsync();
    }
}
