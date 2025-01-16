using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VectorSite.BL.DTO.AuthControllerDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Models;

namespace VectorSite.Services
{
    public class AuthService(
        IUserService userService,
        IConfiguration configuration,
        UserManager<User> userManager
    ) : IAuthService
    {

        public async Task<(int, string)> Registration(RegisterRequestDTO request, string role)
        {
            try
            {
                await userService.CreateUser(request, role);
                return (200, "Success");
            }
            catch (UserAlreadyExistException ex)
            {
                return (409, ex.Message);
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }

        }

        public async Task<(int, string)> Login(LoginRequestDTO request)
        {
            var user = userService.GetUsersQuery().FirstOrDefault(user => user.Email!.Equals(request.Email));

            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return (0, "Incorrect email or password.");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? "Unknown"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
            }

            throw new SecurityTokenException("Invalid token or user ID not found in token.");
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
