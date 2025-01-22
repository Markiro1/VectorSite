using AutoMapper;
using VectorSite.BL.DTO.SubscriptionServiceDTO.Response;
using VectorSite.BL.DTO.UserDTO;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.PaymentServiceDTO.Response
{
    public class PaymentResponseDTO : IMapWith<Payment>
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public decimal Price { get; set; }

        public UserShortDTO? User { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentResponseDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Subscription.User));
        }
    }
}
