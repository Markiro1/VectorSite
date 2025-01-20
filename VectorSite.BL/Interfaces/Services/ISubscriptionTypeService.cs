using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionTypeService
    {
        SubscriptionType GetTypeById(int id);

        List<SubscriptionType> GetAllSubscriptionType();
    }
}
