using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; } = null!;

        public DateTime Time { get; set; } = DateTime.Now;

        public SubscriptionType Type { get; set; } = null!;
    }
}
