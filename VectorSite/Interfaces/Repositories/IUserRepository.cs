using VectorSite.Models;

namespace VectorSite.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        void CheckUserExistsByPhoneNumber(string phoneNumber);
    }
}
