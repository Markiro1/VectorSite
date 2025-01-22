using VectorSite.BL.DTO.SubscriptionServiceDTO.Request;
using VectorSite.BL.DTO.SubscriptionServiceDTO.Response;

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
