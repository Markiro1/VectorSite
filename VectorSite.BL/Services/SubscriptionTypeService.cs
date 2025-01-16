using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionTypeService(
        ISubscriptionTypeRepository subscriptionTypeRepository
    ) : ISubscriptionTypeService
    {
        public SubscriptionType GetTypeById(int id)
        {
            return subscriptionTypeRepository.GetTypeById(id);
        }
    }
}
