using VectorSite.BL.DTO.AdminServiceDTO;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<AdminShortUserDTO>> GetAllAdminShortUsers(int page);
    }
}
