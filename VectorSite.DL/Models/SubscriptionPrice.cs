using System.ComponentModel.DataAnnotations;

namespace VectorSite.DL.Models
{
    public class SubscriptionPrice
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType Type { get; set; } = null!;

        public decimal Price { get; set; } = decimal.Zero;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
