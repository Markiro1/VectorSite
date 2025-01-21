namespace VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request
{
    public class SubPriceUpdateRequestDTO
    {
        public int? TypeId { get; set; } = null;

        public decimal? Price { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;
    }
}
