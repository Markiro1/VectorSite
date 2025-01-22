using AutoMapper;
using VectorSite.BL.DTO.UserDTO;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.CheckoutServiceDTO
{
    public class CheckoutDTO : IMapWith<Checkout>
    {
        public string Status { get; set; } = string.Empty;

        public UserShortDTO User { get; set; } = null!;

        public decimal Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Checkout, CheckoutDTO>();
        }
    }
}
