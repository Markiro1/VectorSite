using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VectorSite.DL.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public Subscription Subscription { get; set; } = null!;

        public decimal Price { get; set; } = decimal.Zero;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
