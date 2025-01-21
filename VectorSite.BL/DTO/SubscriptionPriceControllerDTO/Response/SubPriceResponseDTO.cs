using AutoMapper;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response
{
    public class SubPriceResponseDTO : IMapWith<SubscriptionPrice>
    {
        public decimal Price { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubscriptionPrice, SubPriceResponseDTO>();
        }
    }
}
