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

        //TODO: Нормальні статуси
        public string Status { get; set; } = null!;

        public DateTime Time { get; set; } = DateTime.Now;
    }
}
