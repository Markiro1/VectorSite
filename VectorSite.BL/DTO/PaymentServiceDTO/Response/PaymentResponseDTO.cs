using AutoMapper;
using VectorSite.BL.DTO.UserDTO;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.PaymentServiceDTO.Response
{
    public class PaymentResponseDTO : IMapWith<Payment>
    {
        public UserShortDTO? User { get; set; }

        public DateTime Time { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentResponseDTO>();
        }
    }
}
