using VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Request;
using VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Response;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionTypeService
    {
        SubTypeResponseDTO GetById(int id);

        List<SubTypeResponseDTO> GetAll();

        void Create(SubTypeCreateRequestDTO type);

        void Update(int typeId, SubTypeUpdateRequestDTO updateDTO);
    }
}
