using System.ComponentModel.DataAnnotations;

namespace VectorSite.DL.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType Type { get; set; } = null!;

        public User User { get; set; } = null!;

        public bool IsCancelled { get; set; } = false;

        public bool IsPayed { get; set; } = false;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
