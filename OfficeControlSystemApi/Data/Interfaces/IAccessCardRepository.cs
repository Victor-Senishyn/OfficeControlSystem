using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Data.Interfaces
{
    public interface IAccessCardRepository
    {
        Task<IQueryable<AccessCard>> GetAsync(AccessCardFilter accessCardFilter);
        Task AddAsync(AccessCard entity);
        Task CommitAsync();
        Task DeleteAsync(AccessCard entity);
    }
}
