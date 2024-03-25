using FakeItEasy;
using FluentAssertions;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OfficeControlSystemApiTest.Services.Commands
{
    public class CreateEmployeeCommandTests
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
        }

        [Theory]
        [InlineData("FirstName", "SecondName", AccessLevel.Low)]
        [InlineData("FirstName", "SecondName", AccessLevel.Medium)]
        [InlineData("FirstName", "SecondName", AccessLevel.High)]
        public async Task CreateEmployeeCommand_ExecuteAsync_ReturnsEmployeeDto(
            string firstName, 
            string lastName, 
            AccessLevel accessLevel)
        {
            // Arrange
            var createEmployeeCommand = new CreateEmployeeCommand(_employeeRepository);
            var employeeDto = new EmployeeDto { FirstName = firstName, LastName = lastName };

            // Act
            var result = await createEmployeeCommand.ExecuteAsync(
                employeeDto, 
                accessLevel, 
                CancellationToken.None);

            // Assert
            ((int)accessLevel).Should().BeGreaterOrEqualTo(0);
            ((int)accessLevel).Should().BeLessThan(4);
            result.Should().NotBeNull();
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }

        [Fact]
        public async Task CreateEmployeeCommand_ExecuteAsync_ThrowsArgumentNullException_WhenEmployeeDtoIsNull()
        {
            // Arrange
            var createEmployeeCommand = new CreateEmployeeCommand(_employeeRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => createEmployeeCommand.ExecuteAsync(null, AccessLevel.Low, CancellationToken.None));
        }
    }
}
