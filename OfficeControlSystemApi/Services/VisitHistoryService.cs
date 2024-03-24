using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data.Repositories;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class VisitHistoryService : IVisitHistoryService
    {
        private readonly VisitHistoryRepository _visitHistoryRepository;

        public VisitHistoryService(AppDbContext context)
        {
            _visitHistoryRepository = new VisitHistoryRepository(context);
        }

        public async Task<VisitHistoryDto> UpdateExitDateTime(
            long visitHistoryId, 
            CancellationToken cancellationToken = default)
        {
            var visitHistory = (await _visitHistoryRepository.GetAsync(
                new VisitHistoryFilter() { 
                Id = visitHistoryId })).SingleOrDefault();

            if (visitHistory == null)
                throw new ArgumentException($"AccessHistory with id {visitHistoryId} not found");

            visitHistory.ExitDateTime = DateTimeOffset.UtcNow;
            await _visitHistoryRepository.CommitAsync();

            return new VisitHistoryDto()
            {
                Id = visitHistory.Id,
                AccessCardId = visitHistory.AccessCardId,
                VisitDateTime = visitHistory.VisitDateTime,
                ExitDateTime = visitHistory.ExitDateTime
            };
        }
    }
}
