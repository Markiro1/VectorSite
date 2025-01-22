using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VectorSite.DL.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType SubType { get; set; } = null!;

        public virtual int SubTypeId { get; set; }

        public User User { get; set; } = null!;

        public bool IsCancelled { get; set; } = false;

        public Payment? Payment { get; set; } = null!;

        public DateTime? DateFrom { get; set; } = DateTime.Now.ToUniversalTime();

        public DateTime? DateTo { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
