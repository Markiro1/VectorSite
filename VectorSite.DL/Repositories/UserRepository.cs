using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.DL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext context;

        public UserRepository(IDbContext context)
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

        public User GetUserById(string id)
        {
            var user = context.Users
               .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }


        public IQueryable<User> GetUsersQuery()
        {
            return context.Users.AsQueryable();
        }
    }
}
