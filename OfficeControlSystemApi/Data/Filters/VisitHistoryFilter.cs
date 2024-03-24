using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeControlSystemApi.Data.Filters
{
    public class VisitHistoryFilter
    {
        public long? Id { get; set; }
        [ForeignKey(nameof(AccessCardId))]
        public long? AccessCardId { get; set; }
        public DateTimeOffset? VisitDateTime { get; set; }
        public DateTimeOffset? ExitDateTime { get; set; }
    }
}
