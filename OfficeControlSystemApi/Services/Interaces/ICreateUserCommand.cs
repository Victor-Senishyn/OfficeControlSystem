using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Identity;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface ICreateUserCommand
    {
        Task ExecuteAsync(UserCreationModel userModel, UserRole role, CancellationToken cancellationToken = default);
    }
}
