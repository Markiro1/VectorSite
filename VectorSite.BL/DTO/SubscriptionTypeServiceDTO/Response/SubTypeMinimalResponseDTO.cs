using AutoMapper;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Response
{
    public class SubTypeMinimalResponseDTO : IMapWith<SubscriptionType>
    {
        public string Name { get; set; } = string.Empty;

        public int Days { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubscriptionType, SubTypeMinimalResponseDTO>();
        }
    }
}
