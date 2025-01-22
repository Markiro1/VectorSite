using VectorSite.BL.DTO.PaymentServiceDTO.Response;

namespace VectorSite.BL.Interfaces.Services
{
    public interface IPaymentService
    {
        PaymentResponseDTO Create(int checkoutId);

        PaymentSimpleResponseDTO GetById(int paymentId);

        List<PaymentSimpleResponseDTO> GetAll();
    }
}
