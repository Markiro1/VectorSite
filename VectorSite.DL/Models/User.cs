using Microsoft.AspNetCore.Identity;

namespace VectorSite.DL.Models
{
    public class User : IdentityUser
    {
        public List<Subscription> Subscriptions { get; set; } = null!;
    }
}
