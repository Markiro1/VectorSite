using VectorSite.BL.DTO.AuthControllerDTO;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(RegisterRequestDTO user, string role);

        User GetUserByEmail(string email);
    }
}
