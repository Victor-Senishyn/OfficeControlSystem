using System.ComponentModel.DataAnnotations;

namespace OfficeControlSystemApi.Models.Identity
{
    public class UserCreationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
