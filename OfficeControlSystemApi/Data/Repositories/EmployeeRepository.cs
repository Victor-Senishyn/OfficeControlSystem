using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models.Interface;
using OfficeControlSystemApi.Models;
using System.Collections.Generic;
using System.Linq;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data.Filters;

namespace OfficeControlSystemApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Employee>> GetAsync(EmployeeFilter employeeFilter)
        {
            var query = _dbContext.Employees.AsQueryable();

            if(employeeFilter.Id != null)
                query = query.Where(employee => employee.Id == employeeFilter.Id);
            else if(employeeFilter.FirstName != null)
                query = query.Where(employee => employee.FirstName == employeeFilter.FirstName);
            else if(employeeFilter.LastName != null)
                query = query.Where(employee => employee.LastName == employeeFilter.LastName);

            return query;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAsync(Employee entity)
        {
            await _dbContext.Set<Employee>().AddAsync(entity);
        }

        public async Task DeleteAsync(Employee entity)
        {
            _dbContext.Set<Employee>().Remove(entity);
        }
    }
}
