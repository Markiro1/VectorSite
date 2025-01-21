using VectorSite.BL.DTO.SubscriptionTypeControllerDTO;
using VectorSite.DL.Models;

namespace VectorSite.BL.Interfaces.Services
{
    public interface ISubscriptionTypeService
    {
        SubscriptionType GetById(int id);

        List<SubscriptionType> GetAll();

        void Create(SubscriptionTypeCreateDTO type);

        void Update(int typeId, SubscriptionTypeUpdateDTO updateDTO);
    }
}
