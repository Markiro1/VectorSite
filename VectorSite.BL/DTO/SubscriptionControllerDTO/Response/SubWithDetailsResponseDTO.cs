using AutoMapper;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionControllerDTO.Response
{
    public class SubWithDetailsResponseDTO : IMapWith<Subscription>
    {
        //public SubTypeResponseDTO? SubType { get; set; }

        public bool IsCancelled { get; set; } = false;

        public bool IsPayed { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubWithDetailsResponseDTO>()
                //.ForMember(dest => dest.SubType, opt => opt.MapFrom(src => src.SubType))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
                .ForMember(dest => dest.IsPayed, opt => opt.MapFrom(src => src.IsPayed))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));
        }
    }
}
