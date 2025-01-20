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

        [JsonIgnore]
        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        [JsonIgnore]
        public List<SubscriptionPrice> Prices { get; set; } = new List<SubscriptionPrice>();

        [JsonIgnore]
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
