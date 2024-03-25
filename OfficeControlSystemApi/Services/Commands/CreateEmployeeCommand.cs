using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        private readonly IEmployeeRepository _employeeRepository;


        public CreateEmployeeCommand(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> ExecuteAsync(
            EmployeeDto employeeDto, 
            AccessLevel accessLevel, 
            CancellationToken cancellationToken = default)
        {
            if(employeeDto == null)
                throw new ArgumentNullException(nameof(employeeDto));

            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
            };

            var accessCard = new AccessCard
            {
                AccessLevel = accessLevel,
                EmployeeId = employee.Id
            };

            var visitHistory = new VisitHistory
            {
                AccessCardId = accessCard.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };

            accessCard.VisitHistories.Add( visitHistory );
            employee.AccessCards.Add( accessCard );

            await _employeeRepository.AddAsync(employee);

            await _employeeRepository.CommitAsync();
            employeeDto.Id = employee.Id;

            return employeeDto;
        }
    }

}
