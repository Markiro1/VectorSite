using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response;
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
