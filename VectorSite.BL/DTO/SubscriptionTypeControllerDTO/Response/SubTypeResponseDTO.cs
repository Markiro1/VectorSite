using AutoMapper;
using VectorSite.BL.DTO.PaymentControllerDTO.Response;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response
{
    public class SubTypeResponseDTO : IMapWith<SubscriptionType>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Days { get; set; }

        //public List<SubPriceResponseDTO>? Prices { get; set; }

        //public List<PaymentResponseDTO>? Payments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubscriptionType, SubTypeResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
