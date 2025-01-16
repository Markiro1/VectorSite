using VectorSite.DL.Models;

namespace VectorSite.DL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(string id);

        void CheckUserExistsByPhoneNumber(string phoneNumber);

        IQueryable<User> GetUsersQuery();
    }
}
