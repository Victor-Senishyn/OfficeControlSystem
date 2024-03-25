using FakeItEasy;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeControlSystemApi.Data.Filters;

namespace OfficeControlSystemApiTest.Services
{
    public class VisitHistoryServiceTests
    {
        [Fact]
        public async Task UpdateExitDateTime_ValidId_Success()
        {
            // Arrange
            long visitHistoryId = 123;
            var cancellationToken = CancellationToken.None;

            var repository = A.Fake<IVisitHistoryRepository>(); 
            var visitHistory = new VisitHistory { Id = visitHistoryId };

            A.CallTo(() => repository.GetAsync(A<VisitHistoryFilter>.Ignored))
                .Returns(new[] { visitHistory }.AsQueryable());

            var service = new VisitHistoryService(repository);

            // Act
            var result = await service.UpdateExitDateTime(visitHistoryId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(visitHistory.Id, result.Id);
            Assert.Equal(visitHistory.AccessCardId, result.AccessCardId);
            Assert.Equal(visitHistory.VisitDateTime, result.VisitDateTime);
            Assert.NotEqual(default(DateTimeOffset), result.ExitDateTime);

            A.CallTo(() => repository.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateExitDateTime_InvalidId_ThrowsException()
        {
            // Arrange
            long visitHistoryId = 123;
            var cancellationToken = CancellationToken.None;

            var repository = A.Fake<IVisitHistoryRepository>(); 

            A.CallTo(() => repository.GetAsync(A<VisitHistoryFilter>.Ignored))
                .Returns(Enumerable.Empty<VisitHistory>().AsQueryable());

            var service = new VisitHistoryService(repository);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateExitDateTime(visitHistoryId, cancellationToken));
        }
    }
}
