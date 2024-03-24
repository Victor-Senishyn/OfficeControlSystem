using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Models.Enums;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface ICreateEmployeeCommand
    {
        Task<EmployeeDto> ExecuteAsync(EmployeeDto employeeDto, AccessLevel accessLevel, CancellationToken cancellationToken);
    }
}
