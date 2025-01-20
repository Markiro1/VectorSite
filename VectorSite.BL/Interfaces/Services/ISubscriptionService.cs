using VectorSite.BL.DTO.SubscriptionControllerDTO;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionService
    {
        void Create(int subTypeId, string userId);

        void Update(int subId, SubscriptionUpdateDTO updateDTO); //Працює не правильно

        List<SubscriptionDTO> GetAllSubs();

        SubscriptionWithDetailsDTO GetSubscriptionByUserId(string userId);

    }
}
