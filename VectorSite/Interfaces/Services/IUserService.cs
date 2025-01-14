using VectorSite.DTO.AuthControllerDTO;
using VectorSite.Models;

namespace VectorSite.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(RegisterRequestDTO user, string role);

        User GetUserByEmail(string email);
    }
}
