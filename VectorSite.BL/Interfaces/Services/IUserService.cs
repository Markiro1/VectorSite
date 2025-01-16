using VectorSite.BL.DTO.AuthControllerDTO;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(RegisterRequestDTO user, string role);

        User GetUserById(string id);

        IQueryable<User> GetUsersQuery();
    }
}
