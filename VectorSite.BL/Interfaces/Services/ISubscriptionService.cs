using VectorSite.BL.DTO.SubscriptionControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionControllerDTO.Response;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionService
    {
        void Create(int subTypeId, string userId);

        void Update(int subId, SubUpdateRequestDTO updateDTO); //Працює не правильно

        List<SubResponseDTO> GetAllSubs();

        SubWithDetailsResponseDTO GetByUserId(string userId);

    }
}
