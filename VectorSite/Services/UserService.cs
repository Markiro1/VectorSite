using VectorSite.DTO.AuthControllerDTO;
using VectorSite.Exceptions.UserExceptions;
using VectorSite.Interfaces.Repositories;
using VectorSite.Interfaces.Services;
using VectorSite.Models;

namespace VectorSite.Services
{
    public class UserService : IUserService
    {
        private readonly NpgsqlDbContext context;

        private readonly IUserRepository userRepository;

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;

        private readonly IPasswordService passwordService;

        public UserService(
            NpgsqlDbContext context, 
            IUserRepository userRepository, 
            ISubscriptionTypeRepository subscriptionTypeRepository, 
            IPasswordService passwordService)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            this.passwordService = passwordService;
        }

        public void CreateUser(RegisterRequestDTO request) 
        { 
           userRepository.CheckUserExistsByPhoneNumber(request.PhoneNumber);

            var user = new User() { 
                Name = request.Name, 
                Email = request.Email,
                PhoneNumber = request.PhoneNumber, 
                Password = passwordService.HashPassword(request.Password),
                Role = "user"
            };

            context.Add(user);
            context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }
    }
}
