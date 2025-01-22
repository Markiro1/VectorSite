using AutoMapper;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.UserDTO
{
    public class UserShortDTO : IMapWith<User>
    {
        public String Name { get; set; } = String.Empty;

        public String Email { get; set; } = String.Empty;

        public String PhoneNumber { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserShortDTO>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
