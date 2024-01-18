using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.UnitofWork
{
    public interface IUnitOfwork : IDisposable
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
