using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IMockupService
    {
        public (User User, string Password, string Role) GenerateUser();

        public string GenerateEmail(string guid);

        public string GenerateUserName(string guid);

        public string GeneratePassword(string guid);

        public string GeneratePhone();
    }
}
