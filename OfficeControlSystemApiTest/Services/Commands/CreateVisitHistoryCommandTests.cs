using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeControlSystemApi.Data.Repositories;
using OfficeControlSystemApi.Services.Commands;
using FakeItEasy;

namespace OfficeControlSystemApiTest.Services.Commands
{
    public class CreateVisitHistoryCommandTests
    {
        private readonly IAccessCardRepository _accessCardRepository;

        public CreateVisitHistoryCommandTests()
        {
            _accessCardRepository = A.Fake<IAccessCardRepository>();
        }

        [Fact]
        public async Task ExecuteAsync_WithValidAccessCardId_ReturnsVisitHistoryDto()
        {
            // Arrange
            var accessCardId = 1;
            var accessCard = new AccessCard { Id = accessCardId };
            A.CallTo(() => _accessCardRepository.GetAsync(A<AccessCardFilter>._))
                .Returns(Task.FromResult(new List<AccessCard> { accessCard }.AsQueryable()));

            var createVisitHistoryCommand = new CreateVisitHistoryCommand(_accessCardRepository);

            // Act
            var result = await createVisitHistoryCommand.ExecuteAsync(accessCardId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.AccessCardId.Should().Be(accessCardId);
            result.VisitDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, precision: new TimeSpan(100000));
        }
    }
}
