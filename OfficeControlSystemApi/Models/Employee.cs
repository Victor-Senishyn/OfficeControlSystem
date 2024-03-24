using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models
{
    [Table(nameof(Employee))]
    public class Employee : IEntity
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AccessCard> AccessCards { get; set; } = new List<AccessCard>();
    }
}
