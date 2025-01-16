using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionService(
        IUserService userService,
        ISubscriptionTypeService subscriptionTypeService,
        IDbContext context
    ) : ISubscriptionService
    {
        // TODO: Додати логіку в майбутньому, щоб виключати останню підписку (IsCancelled)
        public void Create(int subTypeId, string userId)
        {
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var subType = subscriptionTypeService.GetTypeById(subTypeId);
            if (subType == null)
            {
                throw new SubscriptionTypeNotFoundException(subTypeId);
            }

            var sub = new Subscription()
            {
                User = user,
                Type = subType
            };

            context.Subscriptions.Add(sub);
            context.SaveChanges();
        }
    }
}
