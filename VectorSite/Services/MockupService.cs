using System.Text;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Models;

namespace VectorSite.Services
{
    public class MockupService(IDbContext context) : IMockupService
    {
        public (User User, string Password, string Role) GenerateUser()
        {
            string guid = Guid.NewGuid().ToString();

            User user = new User()
            {
                Id = guid,
                Email = GenerateEmail(guid),
                UserName = GenerateUserName(guid),
                PhoneNumber = GeneratePhone(),
            };
            user.Subscriptions = GenerateSubscriptions(1, user, true);

            return (user, GeneratePassword(guid), GenerateRole());
        }

        public List<Subscription> GenerateSubscriptions(int numOfSubs, User user, bool isCompleteActive)
        {
            var subs = new List<Subscription>();

            for (int i = 0; i < numOfSubs; i++)
            {
                subs.Add(GenerateSubscription(user, isCompleteActive));
            }

            return subs;
        }

        public Subscription GenerateSubscription(User user, bool isCompleteActive)
        {
            Random random = new Random();
            Subscription sub = new Subscription();
            sub.User = user;

            var subTypes = context.SubscriptionTypes.ToList();

            sub.SubType = subTypes[random.Next(subTypes.Count)];
            
            if(random.Next(2) == 1 || isCompleteActive)
            {
                sub.Payment = GeneratePayment(sub, isCompleteActive);
                sub.DateFrom = sub.Payment.Date;
                sub.DateTo = sub.DateFrom?.AddDays(sub.SubType.Days);
            }

            if (random.Next(2) == 1 && !isCompleteActive)
            {
                sub.IsCancelled = true;
            }

            return sub;
        }

        public Payment GeneratePayment(Subscription sub, bool isActive)
        {
            Random random = new Random();
            var paymentDate = isActive ? DateTime.UtcNow.AddDays(random.Next(-20, 0)) : DateTime.UtcNow.AddDays(random.Next(-90, 90));

            return new Payment()
            {
                Subscription = sub,
                SubscriptionId = sub.Id,
                Price = sub.SubType.Prices.FirstOrDefault(p => paymentDate >= p.DateFrom && paymentDate < p.DateTo)?.Price ?? 0,
                Date = paymentDate,
            };
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
