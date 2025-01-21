using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionPriceService
    {
        void Create(SubPriceCreateRequestDTO priceDTO);

        void Update(int priceId, SubPriceUpdateRequestDTO priceDTO);

        SubscriptionPrice GetById(int priceId);

        List<SubscriptionPrice> GetAll();
    }
}
