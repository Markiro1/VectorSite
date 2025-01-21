using VectorSite.BL.DTO.SubscriptionPriceControllerDTO;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionPriceService
    {
        void Create(SubscriptionPriceCreateDTO priceDTO);

        SubscriptionPrice GetById(int priceId);

        List<SubscriptionPrice> GetAll();
    }
}
