using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VectorSite.DL.Models
{
    public class SubscriptionType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Days { get; set; }

        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public List<SubscriptionPrice> Prices { get; set; } = new List<SubscriptionPrice>();

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
