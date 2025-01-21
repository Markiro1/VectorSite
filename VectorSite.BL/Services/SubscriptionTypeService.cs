using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionTypeService(
        IDbContext context
    ) : ISubscriptionTypeService
    {
        public void Create(SubTypeCreateRequestDTO type)
        {
            var newType = new SubscriptionType
            {
                Name = type.Name.ToUpper(),
                Days = type.Days,
            };

            if (context.SubscriptionTypes.Any(t => t.Name.Equals(type.Name)))
            {
                throw new SubscriptionTypeAlreadyExistException(type.Name);
            }

            context.SubscriptionTypes.Add(newType);
            context.SaveChanges();
        }

        public List<SubscriptionType> GetAll()
        {
            return context.SubscriptionTypes
                .Include(t => t.Subscriptions)
                .Include(t => t.Payments)
                .Include(t => t.Prices)
                .ToList();
        }

        public SubscriptionType GetById(int id)
        {
            var type = context.SubscriptionTypes
                .First(type => type.Id == id);

            if (type == null)
            {
                throw new SubscriptionTypeNotFoundException(id);
            }

            return type;
        }

        public void Update(int typeId, SubTypeUpdateRequestDTO updateDTO)
        {
            var type = GetById(typeId);

            type.Days = updateDTO.Days;
            context.SaveChanges();
        }
    }
}
