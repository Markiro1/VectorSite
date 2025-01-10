using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class SubscriptionPrice
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType Type { get; set; } = null!;

        public Decimal Price { get; set; } = Decimal.Zero;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
