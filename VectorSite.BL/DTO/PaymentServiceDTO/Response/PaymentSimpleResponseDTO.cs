using AutoMapper;
using VectorSite.BL.DTO.UserDTO;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.PaymentServiceDTO.Response
{
    public class PaymentSimpleResponseDTO : IMapWith<Payment>
    {
        public decimal Price { get; set; }

        public UserShortDTO? User { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentSimpleResponseDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Subscription.User));
        }
    }
}
