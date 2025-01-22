using AutoMapper;
using VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response
{
    public class SubPriceWithDetailsResponseDTO : IMapWith<SubscriptionPrice>
    {
        public decimal? Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SubTypeMinimalResponseDTO? Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubscriptionPrice, SubPriceWithDetailsResponseDTO>();
        }
    }
}
