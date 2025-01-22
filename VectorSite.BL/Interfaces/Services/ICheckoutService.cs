using VectorSite.BL.DTO.CheckoutServiceDTO;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ICheckoutService
    {
        CheckoutSimpleDTO CreateCheckout(int subTypeId, string userId);

        List<CheckoutDTO> GetAll();

        CheckoutDTO GetById(int checkoutId);
    }
}
