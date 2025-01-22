using System.ComponentModel.DataAnnotations;

namespace VectorSite.DL.Models
{
    public class SubscriptionPrice
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType Type { get; set; } = null!;

        public decimal Price { get; set; } = decimal.Zero;

        public DateTime DateFrom { get; set; } = DateTime.Now.ToUniversalTime();

        public DateTime DateTo { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
