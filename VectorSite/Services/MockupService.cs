using System.Text;
using VectorSite.Interfaces.Services;
using VectorSite.Models;

namespace VectorSite.Services
{
    public class MockupService : IMockupService
    {
        public (User User, string Password, string Role) GenerateUser()
        {
            string guid = Guid.NewGuid().ToString();

            return (new User()
            {
                Id = guid,
                Email = GenerateEmail(guid),
                UserName = GenerateUserName(guid),
                PhoneNumber = GeneratePhone()
            }, GeneratePassword(guid), GenerateRole());
        }

        public string GenerateEmail(string guid)
        {
            return $"mockup.email.{guid}@gmail.com";
        }

        public string GenerateUserName(string guid)
        {
            return $"username_{guid}";
        }

        public string GeneratePassword(string guid)
        {
            return guid;
        }

        public string GeneratePhone()
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                stringBuilder.Append(random.Next(0, 10));
            }

            return "+380" + stringBuilder.ToString();
        }

        public string GenerateRole()
        {
            Random rand = new Random();

            return rand.Next(0, 2) == 0 ? "Admin" : "User";
        }
    }
}
