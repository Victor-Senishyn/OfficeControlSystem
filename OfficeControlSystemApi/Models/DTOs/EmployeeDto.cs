using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models.DTOs
{
    public class EmployeeDto : IEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
