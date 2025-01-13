using VectorSite.Exceptions.UserExceptions;
using VectorSite.Interfaces.Repositories;
using VectorSite.Models;

namespace VectorSite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlDbContext context;

        public UserRepository(NpgsqlDbContext context)
        {
            this.context = context;
        }

        public void CheckUserExistsByPhoneNumber(string phoneNumber)
        {
            bool userExists = context.Users
               .Any(u => u.PhoneNumber == phoneNumber);

            if (userExists)
            {
                throw new UserAlreadyExistException(phoneNumber);
            }
        }

        public User GetUserByEmail(string email)
        {
            var user = context.Users
               .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new UserNotFoundException(email);
            }

            return user;
        }
    }
}
