using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VectorSite.DL.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public Subscription Subscription { get; set; } = null!;

        public decimal Price { get; set; } = decimal.Zero;

        //TODO: Нормальні статуси
        public string Status { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
