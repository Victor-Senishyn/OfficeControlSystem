using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateVisitHistoryCommand : ICreateVisitHistoryCommand
    {
        private readonly IAccessCardRepository _accessCardRepository;

        public CreateVisitHistoryCommand(
            IAccessCardRepository accessCardRepository
            )
        {
            _accessCardRepository = accessCardRepository;
        }

        public async Task<VisitHistoryDto> ExecuteAsync(
            long accessCardId, 
            CancellationToken cancellationToken)
        {
            var accessCard = (await _accessCardRepository.GetAsync(new AccessCardFilter() { Id = accessCardId })).SingleOrDefault();

            if (accessCard == null)
                throw new ArgumentException($"Access Card by Id {accessCardId} not found");

            var visitHistory = new VisitHistory
            {
                AccessCardId = accessCard.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };

            accessCard.VisitHistories.Add( visitHistory );
            await _accessCardRepository.CommitAsync();

            var visitHistoryDto = new VisitHistoryDto
            {
                Id = visitHistory.Id,
                AccessCardId = visitHistory.AccessCardId,
                VisitDateTime = visitHistory.VisitDateTime,
                ExitDateTime = visitHistory.ExitDateTime
            };

            return visitHistoryDto;
        }
    }
}
