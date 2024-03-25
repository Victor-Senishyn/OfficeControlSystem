using Microsoft.AspNetCore.Identity;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Identity;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;

        public CreateUserCommand(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(
            UserCreationModel userModel,
            UserRole role,
            CancellationToken cancellationToken = default)
        {
            var roleName = role == UserRole.Administrator ? "Administrator" : "User";

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            var user = new User
            {
                UserName = userModel.Email,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            if (!await _userManager.IsInRoleAsync(user, roleName))
                await _userManager.AddToRoleAsync(user, roleName);
            
            await _userRepository.CommitAsync();
        }
    }
}
