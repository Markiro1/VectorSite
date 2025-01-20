using VectorSite.BL.DTO.SubscriptionTypeControllerDTO;

namespace VectorSite.BL.DTO.SubscriptionControllerDTO
{
    public class SubscriptionWithDetailsDTO
    {
        public SubscriptionTypeDTO? SubType { get; set; }

        public bool IsCancelled { get; set; } = false;

        public bool IsPayed { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
