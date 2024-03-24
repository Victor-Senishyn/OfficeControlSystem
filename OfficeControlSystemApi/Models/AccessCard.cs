using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeControlSystemApi.Models.Interface;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Identity;

namespace OfficeControlSystemApi.Models
{
    [Table(nameof(AccessCard))]
    public class AccessCard : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public ICollection<VisitHistory> VisitHistories { get; set; } = new List<VisitHistory>();
    }
}
