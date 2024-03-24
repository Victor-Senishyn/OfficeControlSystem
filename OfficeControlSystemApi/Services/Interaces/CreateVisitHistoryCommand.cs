using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface ICreateVisitHistoryCommand
    {
        Task<VisitHistoryDto> ExecuteAsync(long accessCardId, CancellationToken cancellationToken);
    }
}
