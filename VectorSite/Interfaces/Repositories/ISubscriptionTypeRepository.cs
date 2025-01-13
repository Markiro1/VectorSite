using VectorSite.Models;

namespace VectorSite.Interfaces.Repositories
{
    public interface ISubscriptionTypeRepository
    {
        SubscriptionType GetTypeByName(string name);
    }
}
