using AutoMapper;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Response
{
    public class SubTypeResponseDTO : IMapWith<SubscriptionType>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Days { get; set; }

        // TODO I don`t think we need price here
        public List<SubPriceResponseDTO>? Prices { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubscriptionType, SubTypeResponseDTO>();
        }
    }
}
