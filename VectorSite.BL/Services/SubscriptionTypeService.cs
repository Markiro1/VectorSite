using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Response;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Models;
using System.Diagnostics;

namespace VectorSite.BL.Services
{
    public class SubscriptionTypeService(
        IDbContext context,
        IMapper mapper
    ) : ISubscriptionTypeService
    {
        public void Create(SubTypeCreateRequestDTO type)
        {
            var newType = new SubscriptionType
            {
                Name = type.Name,
                Days = type.Days,
            };

            if (context.SubscriptionTypes.Any(t => t.Name.ToUpper() == newType.Name.ToUpper()))
            {
                throw new SubscriptionTypeAlreadyExistException(type.Name);
            }

            context.SubscriptionTypes.Add(newType);
            context.SaveChanges();
        }

        public List<SubTypeResponseDTO> GetAll()
        {
            return context.SubscriptionTypes
                .Include(t => t.Prices)
                .ProjectTo<SubTypeResponseDTO>(mapper.ConfigurationProvider)
                .ToList();
        }

        public SubTypeResponseDTO GetById(int id)
        {
            var type = context.SubscriptionTypes
                .Include(t => t.Prices)
                .First(type => type.Id == id);

            if (type == null)
            {
                throw new SubscriptionTypeNotFoundException(id);
            }

            return mapper.Map<SubTypeResponseDTO>(type);
        }

        public void Update(int typeId, SubTypeUpdateRequestDTO updateDTO)
        {
            var type = GetById(typeId);

            type.Days = updateDTO.Days;
            context.SaveChanges();
        }
    }
}
