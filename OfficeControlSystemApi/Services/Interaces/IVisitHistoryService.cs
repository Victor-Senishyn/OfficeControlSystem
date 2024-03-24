using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        Task<VisitHistoryDto> UpdateExitDateTime(long visitHistoryId, CancellationToken cancellationToken);
    }
}
