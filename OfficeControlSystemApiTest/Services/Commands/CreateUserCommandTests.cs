using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Identity;
using OfficeControlSystemApi.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeControlSystemApiTest.Services.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FakeItEasy;
    using Microsoft.AspNetCore.Identity;
    using Xunit;

    public class CreateUserCommandTests
    {
        [Fact]
        public async Task ExecuteAsync_UserCreation_Success()
        {
            // Arrange
            var userManager = A.Fake<UserManager<User>>();
            var roleManager = A.Fake<RoleManager<IdentityRole>>();
            var userRepository = A.Fake<IUserRepository>();

            var createUserCommand = new CreateUserCommand(userManager, roleManager, userRepository);

            var userModel = new UserCreationModel
            {
                Email = "test@example.com",
                Password = "TestPassword123"
            };

            var role = UserRole.User;

            A.CallTo(() => userManager.CreateAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(IdentityResult.Success);
            A.CallTo(() => userManager.IsInRoleAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(true);

            // Act
            await createUserCommand.ExecuteAsync(userModel, role);

            // Assert
            A.CallTo(() => userManager.CreateAsync(A<User>.Ignored, userModel.Password)).MustHaveHappenedOnceExactly();
            A.CallTo(() => userManager.IsInRoleAsync(A<User>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => userRepository.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ExecuteAsync_UserCreation_Fail()
        {
            // Arrange
            var userManager = A.Fake<UserManager<User>>();
            var roleManager = A.Fake<RoleManager<IdentityRole>>();
            var userRepository = A.Fake<IUserRepository>();

            var createUserCommand = new CreateUserCommand(userManager, roleManager, userRepository);

            var userModel = new UserCreationModel
            {
                Email = "test@example.com",
                Password = "TestPassword123"
            };

            var role = UserRole.User;

            var errorDescription = "Password does not meet the requirements.";
            var errors = new List<IdentityError> { new IdentityError { Description = errorDescription } };
            var identityResult = IdentityResult.Failed(errors.ToArray());

            A.CallTo(() => userManager.CreateAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(identityResult);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await createUserCommand.ExecuteAsync(userModel, role));
        }
    }

}
