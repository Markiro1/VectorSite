using System.ComponentModel.DataAnnotations;

namespace VectorSite.DL.Models
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public decimal Amount { get; set; }
    }
}
