using VectorSite.DL.Models;

namespace VectorSite.DL.Interfaces.Repositories
{
    public interface ISubscriptionTypeRepository
    {
        SubscriptionType GetTypeById(int id);
    }
}
