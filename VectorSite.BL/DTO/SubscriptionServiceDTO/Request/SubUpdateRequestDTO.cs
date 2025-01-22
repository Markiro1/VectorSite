namespace VectorSite.BL.DTO.SubscriptionServiceDTO.Request
{
    public class SubUpdateRequestDTO
    {
        public int? SubTypeId { get; set; } = null;

        public bool? IsCancelled { get; set; } = null;

        public bool? IsPayed { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;
    }
}
