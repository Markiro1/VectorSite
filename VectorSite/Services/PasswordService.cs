using Microsoft.AspNetCore.Identity;
using VectorSite.Interfaces.Services;

namespace VectorSite.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> passwordHasher;

        public PasswordService(PasswordHasher<object> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public String HashPassword(string password)
        {
            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
