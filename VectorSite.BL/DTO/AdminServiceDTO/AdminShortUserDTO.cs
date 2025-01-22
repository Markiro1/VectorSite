using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.AdminServiceDTO
{
    public class AdminShortUserDTO : IMapWith<User>
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string CurrentSubscription { get; set; } = string.Empty;

        public string? SubscriptionEndDate { get; set; } = string.Empty;
    }
}
