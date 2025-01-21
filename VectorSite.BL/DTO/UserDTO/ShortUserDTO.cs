using AutoMapper;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.UserDTO
{
    public class ShortUserDTO : IMapWith<User>
    {
        public String Name { get; set; } = String.Empty;

        public String Email { get; set; } = String.Empty;

        public String PhoneNumber { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, ShortUserDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
