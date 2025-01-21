using AutoMapper;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionControllerDTO.Response
{
    public class SubWithDetailsResponseDTO : IMapWith<Subscription>
    {
        public SubTypeResponseDTO? SubType { get; set; }

        public bool IsCancelled { get; set; } = false;

        public bool IsPayed { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubWithDetailsResponseDTO>();
        }
    }
}
