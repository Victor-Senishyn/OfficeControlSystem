using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IQueryable<Employee>> GetAsync(EmployeeFilter employeeFilter);
        Task AddAsync(Employee entity);
        Task CommitAsync();
        Task DeleteAsync(Employee entity);
    }
}
