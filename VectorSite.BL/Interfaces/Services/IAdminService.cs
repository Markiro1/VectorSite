using VectorSite.BL.DTO.AdminControllerDTO;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<AdminShortUserDTO>> GetAllAdminShortUsers(int page);
    }
}
