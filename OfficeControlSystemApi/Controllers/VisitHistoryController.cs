using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly ICreateVisitHistoryCommand _createVisitHistoryCommand;

        public VisitHistoryController(
            IVisitHistoryService visitHistoryService,
            ICreateVisitHistoryCommand createVisitHistoryCommand)
        {
            _visitHistoryService = visitHistoryService;
            _createVisitHistoryCommand = createVisitHistoryCommand;
        }

        [HttpPatch("/exit/{visitHistoryId}")]
        [Authorize(Policy = "AdministratorOrUserPolicy")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(
            long visitHistoryId, 
            CancellationToken cancellationToken)
        {
            return BadRequest("Request canceled due to user action or timeout.");
        }

        [HttpPost("/visit/{accessCardId}")]
        [Authorize(Policy = "AdministratorOrUserPolicy")]
        public async Task<IActionResult> AddVisitHistory(
            long accessCardId, 
            CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _createVisitHistoryCommand.ExecuteAsync(accessCardId, cancellationToken));
        }
    }
}
