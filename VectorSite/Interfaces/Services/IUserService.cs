using VectorSite.DTO.AuthControllerDTO;
using VectorSite.Models;

namespace VectorSite.Interfaces.Services
{
    public interface IUserService
    {
        void CreateUser(RegisterRequestDTO user);

        User GetUserByEmail(string email);
    }
}
