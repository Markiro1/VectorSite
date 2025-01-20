using Microsoft.EntityFrameworkCore;
using VectorSite.DL.Exceptions.SubscriptionExceptios;
using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.DL.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IDbContext context;

        public SubscriptionRepository(IDbContext context)
        {
            this.context = context;
        }

        public List<Subscription> GetAllSubs()
        {
            return context?.Subscriptions
                .Include(s => s.SubType)
                .Include(s => s.User)
                .ToList() ?? [];
        }

        public Subscription GetSubscriptionById(int subscriptionId)
        {
            var sub = context.Subscriptions
                .Include(s => s.SubType)
                .FirstOrDefault(s => s.Id == subscriptionId);
            if (sub == null)
            {
                throw new SubscriptionNotFoundException(subscriptionId);
            }
            return sub;
        }

        public Subscription GetSubscriptionByUserId(string userId)
        {
            var sub = context.Subscriptions
                .Include(s => s.SubType)
                .FirstOrDefault(s => s.User.Id.Equals(userId));
            if (sub == null)
            {
                throw new SubscriptionNotFoundException(userId);
            }
            return sub;
        }
    }
}
