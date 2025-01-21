using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionPriceService
    {
        void Create(SubPriceCreateRequestDTO priceDTO);

        void Update(int priceId, SubPriceUpdateRequestDTO priceDTO);

        SubPriceWithDetailsResponseDTO GetById(int priceId);

        List<SubPriceWithDetailsResponseDTO> GetAll();
    }
}
