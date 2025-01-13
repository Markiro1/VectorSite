using VectorSite.DTO.AuthControllerDTO;
using VectorSite.Exceptions.AuthExceptions;
using VectorSite.Exceptions.UserExceptions;
using VectorSite.Interfaces.Services;
using VectorSite.Util;

namespace VectorSite.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;

        private readonly IPasswordService passwordService;

        public AuthService(IUserService userService, IPasswordService passwordService)
        {
            this.userService = userService;
            this.passwordService = passwordService;
        }

        public void Register(RegisterRequestDTO request)
        {
            userService.CreateUser(request);
        }

        public string Login(LoginRequestDTO request)
        {
            var user = userService.GetUserByEmail(request.Email);

            var result = passwordService.VerifyPassword(user.Password, request.Password);

            if (result == false)
            {
                throw new LoginFailedException();
            }

            return AuthenticationUtil.GenerateJWTAuthentication(user);
        }
    }
}
