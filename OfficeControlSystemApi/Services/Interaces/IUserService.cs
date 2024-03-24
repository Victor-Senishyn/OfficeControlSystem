using OfficeControlSystemApi.Models.Identity;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserCreationModel user, CancellationToken cancellationToken);
        Task CreateAdministratorAsync(UserCreationModel userModel, CancellationToken cancellationToken = default);
    }
}
