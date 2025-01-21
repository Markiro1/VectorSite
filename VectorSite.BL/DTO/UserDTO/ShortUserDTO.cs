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
            profile.CreateMap<User, ShortUserDTO>();
        }
    }
}
