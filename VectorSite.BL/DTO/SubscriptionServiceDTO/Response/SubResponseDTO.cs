using AutoMapper;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionServiceDTO.Response
{
    public class SubResponseDTO : IMapWith<Subscription>
    {
        public int TypeId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public bool IsCancelled { get; set; } = false;

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubResponseDTO>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.SubType.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
