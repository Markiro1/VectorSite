using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VectorSite.DL.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public List<Subscription> Subscriptions { get; set; } = null!;

        [JsonIgnore]
        public List<Payment> Payment { get; set; } = null!;
    }
}
