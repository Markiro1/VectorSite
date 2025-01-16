using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.AdminControllerDTO
{
    public class ShortAdminUserDTO : IMapWith<User>
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string CurrentSubscription { get; set; } = string.Empty;
    }
}
