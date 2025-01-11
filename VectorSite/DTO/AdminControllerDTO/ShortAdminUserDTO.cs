using VectorSite.Common.Mappings;
using VectorSite.Models;

namespace VectorSite.DTO.AdminControllerDTO
{
    public class ShortAdminUserDTO : IMapWith<User>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string CurrentSubscription { get; set; } = string.Empty;
    }
}
