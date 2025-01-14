using Microsoft.AspNetCore.Identity;
using VectorSite.DTO.AuthControllerDTO;
using VectorSite.Exceptions.UserExceptions;
using VectorSite.Interfaces.Repositories;
using VectorSite.Interfaces.Services;
using VectorSite.Models;

namespace VectorSite.Services
{
    public class UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IUserRepository userRepository
    ): IUserService
    {

        public async Task CreateUser(RegisterRequestDTO request, string role) 
        { 
            userRepository.CheckUserExistsByPhoneNumber(request.PhoneNumber);

            var user = new User() { 
                UserName = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber, 
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
            {
                throw new UserCreationFailedException();
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRoleAsync(user, role);

        }

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }
    }
}
