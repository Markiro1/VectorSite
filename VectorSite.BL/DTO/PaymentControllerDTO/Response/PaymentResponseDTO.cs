using AutoMapper;
using VectorSite.BL.DTO.UserDTO;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.PaymentControllerDTO.Response
{
    public class PaymentResponseDTO : IMapWith<Payment>
    {
        public ShortUserDTO User { get; set; }

        public DateTime Time { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentResponseDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
        }
    }
}
