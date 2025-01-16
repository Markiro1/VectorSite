using VectorSite.DL.Models;

namespace VectorSite.DL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        void CheckUserExistsByPhoneNumber(string phoneNumber);
    }
}
