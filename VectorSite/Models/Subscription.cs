using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
