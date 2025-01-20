using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionControllerDTO
{
    public class SubscriptionUpdateDTO
    {
        public int? SubTypeId { get; set; }

        public bool? IsCancelled { get; set; } = false;

        public bool? IsPayed { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
