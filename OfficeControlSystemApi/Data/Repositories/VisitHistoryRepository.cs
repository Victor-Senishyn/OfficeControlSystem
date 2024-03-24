using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositories
{
    public class VisitHistoryRepository : IVisitHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public VisitHistoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<VisitHistory>> GetAsync(VisitHistoryFilter visitHistoryFilter)
        {
            var query = _dbContext.VisitHistories.AsQueryable();

            if (visitHistoryFilter.Id != null)
                query = query.Where(visitHistory => visitHistory.Id == visitHistoryFilter.Id);
            else if (visitHistoryFilter.AccessCardId != null)
                query = query.Where(visitHistory => visitHistory.AccessCardId == visitHistoryFilter.AccessCardId);
            else if (visitHistoryFilter.VisitDateTime != null)
                query = query.Where(visitHistory => visitHistory.VisitDateTime == visitHistoryFilter.VisitDateTime);
            else if (visitHistoryFilter.ExitDateTime != null)
                query = query.Where(visitHistory => visitHistory.ExitDateTime == visitHistoryFilter.ExitDateTime);

            return query;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(VisitHistory entity)
        {
            await _dbContext.Set<VisitHistory>().AddAsync(entity);
        }

        public async Task DeleteAsync(VisitHistory entity)
        {
            _dbContext.Set<VisitHistory>().Remove(entity);
        }
    }
}
