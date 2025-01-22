using AutoMapper;
using VectorSite.BL.DTO.SubscriptionServiceDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.PaymentServiceDTO.Response
{
    public class SimplePaymentResponseDTO : IMapWith<Payment>
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, SimplePaymentResponseDTO>();
        }
    }
}
