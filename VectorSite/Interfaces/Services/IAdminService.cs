using VectorSite.DTO.AdminControllerDTO;

namespace VectorSite.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<ShortAdminUserDTO>> GetAllUsers(int page);
    }
}
