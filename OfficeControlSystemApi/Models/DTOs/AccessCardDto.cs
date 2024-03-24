using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models.DTOs
{
    public class AccessCardDto : IEntity
    {
        public long Id { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
