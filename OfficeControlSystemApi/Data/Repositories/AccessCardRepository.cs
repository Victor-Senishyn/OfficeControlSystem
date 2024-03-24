using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OfficeControlSystemApi.Data.Repositories
{
    public class AccessCardRepository: IAccessCardRepository
    {
        private readonly AppDbContext _dbContext;

        public AccessCardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<AccessCard>> GetAsync(AccessCardFilter accessCardFilter)
        {
            var query = _dbContext.AccessCards.AsQueryable();

            if (accessCardFilter.Id != null)
                query = query.Where(accessCard => accessCard.Id == accessCardFilter.Id);
            else if(accessCardFilter.EmployeeId != null)
                query = query.Where(accessCard => accessCard.EmployeeId == accessCardFilter.EmployeeId);
            else if(accessCardFilter.AccessLevel != null)
                query = query.Where(accessCard => accessCard.AccessLevel == accessCardFilter.AccessLevel);

            return query;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(AccessCard entity)
        {
            await _dbContext.Set<AccessCard>().AddAsync(entity);
        }

        public async Task DeleteAsync(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Remove(entity);
        }
    }
}
