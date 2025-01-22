using VectorSite.BL.DTO.AuthServiceDTO.Request;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegisterRequestDTO registerRequest, string role);

        Task<(int, string)> Login(LoginRequestDTO loginRequest);

        string GetUserIdFromToken(string token);
    }
}
