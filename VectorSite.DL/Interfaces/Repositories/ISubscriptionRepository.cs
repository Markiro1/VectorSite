using VectorSite.DL.Models;

namespace VectorSite.DL.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        List<Subscription> GetAllSubs();

        Subscription GetSubscriptionByUserId(string userId);

        Subscription GetSubscriptionById(int subscriptionId);
    }
}
