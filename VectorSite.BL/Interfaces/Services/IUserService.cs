using VectorSite.BL.DTO.AuthServiceDTO.Request;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(RegisterRequestDTO user, string role);

        User GetUserById(string id);
    }
}
