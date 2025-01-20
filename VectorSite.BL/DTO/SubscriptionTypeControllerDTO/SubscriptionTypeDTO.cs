using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionTypeControllerDTO
{
    public class SubscriptionTypeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Days { get; set; }

        public List<SubscriptionPrice> Prices { get; set; } = new List<SubscriptionPrice>();

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
