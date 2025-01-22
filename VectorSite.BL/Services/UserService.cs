using Microsoft.AspNetCore.Identity;
using VectorSite.BL.DTO.AuthServiceDTO.Request;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IDbContext context
    ) : IUserService
    {

        public async Task CreateUser(RegisterRequestDTO request, string role)
        {
            bool userExists = context.Users
              .Any(u => u.PhoneNumber == request.PhoneNumber);

            if (userExists)
            {
                throw new UserAlreadyExistException(request.PhoneNumber);
            }

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
            var user = context.Users
               .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }
    }
}
