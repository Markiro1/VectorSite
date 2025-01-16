using Microsoft.AspNetCore.Identity;
using VectorSite.BL.DTO.AuthControllerDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserRepository userRepository
    ) : IUserService
    {

        public async Task CreateUser(RegisterRequestDTO request, string role)
        {
            userRepository.CheckUserExistsByPhoneNumber(request.PhoneNumber);

            var user = new User()
            {
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

        public User GetUserById(string id)
        {
            return userRepository.GetUserById(id);
        }

        public IQueryable<User> GetUsersQuery()
        {
            return userRepository.GetUsersQuery();
        }

    }
}
