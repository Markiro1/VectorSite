using AutoMapper;
using VectorSite.BL.DTO.PaymentServiceDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionServiceDTO.Response
{
    public class SubWithDetailsResponseDTO : IMapWith<Subscription>
    {
        public string TypeName { get; set; } = string.Empty;

        public bool IsCancelled { get; set; } = false;

        public PaymentSimpleResponseDTO Payment { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubWithDetailsResponseDTO>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.SubType.Name));
        }
    }
}
