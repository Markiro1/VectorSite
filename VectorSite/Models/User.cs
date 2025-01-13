using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public List<Subscription> Subscriptions { get; set; } = null!;

        public List<Payment> Payment { get; set; } = null!;

        public string Password { get; set; } = string.Empty;
    }
}
