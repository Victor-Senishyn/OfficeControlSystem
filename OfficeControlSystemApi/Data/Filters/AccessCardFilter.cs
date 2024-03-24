using OfficeControlSystemApi.Models.Enums;

namespace OfficeControlSystemApi.Data.Filters
{
    public class AccessCardFilter
    {
        public long? Id { get; set; }
        public long? EmployeeId { get; set; }
        public AccessLevel? AccessLevel { get; set; }
    }
}
