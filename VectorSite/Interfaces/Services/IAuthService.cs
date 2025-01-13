using VectorSite.DTO.AuthControllerDTO;

namespace VectorSite.Interfaces.Services
{
    public interface IAuthService
    {
        void Register(RegisterRequestDTO registerRequest);

        string Login(LoginRequestDTO loginRequest);
    }
}
