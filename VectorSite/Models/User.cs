using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VectorSite.Models
{
    public class User : IdentityUser
    {
        public List<Subscription> Subscriptions { get; set; } = null!;

        public List<Payment> Payment { get; set; } = null!;
    }
}
