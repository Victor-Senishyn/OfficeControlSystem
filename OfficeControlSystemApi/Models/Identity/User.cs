using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeControlSystemApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeControlSystemApi.Models.Identity
{
    [Table(nameof(User))]
    public class User : IdentityUser
    {
        public long EmployeeId { get; set; }
    }
}
