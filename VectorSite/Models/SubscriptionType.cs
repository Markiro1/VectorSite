using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class SubscriptionType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public List<SubscriptionPrice> Prices { get; set; } = new List<SubscriptionPrice>();

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
