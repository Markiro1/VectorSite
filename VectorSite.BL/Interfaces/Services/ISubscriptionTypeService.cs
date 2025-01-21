using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionTypeService
    {
        SubscriptionType GetById(int id);

        List<SubscriptionType> GetAll();

        void Create(SubTypeCreateRequestDTO type);

        void Update(int typeId, SubTypeUpdateRequestDTO updateDTO);
    }
}
