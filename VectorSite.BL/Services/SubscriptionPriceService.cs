using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionPriceExceptions;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionPriceService(
        IDbContext context
    ) : ISubscriptionPriceService
    {
        public void Create(SubPriceCreateRequestDTO priceDTO)
        {
            var type = context.SubscriptionTypes
                .First(type => type.Id == priceDTO.SubTypeId);

            SubscriptionPrice price = new SubscriptionPrice
            {
                Price = priceDTO.Price,
                Type = type,
                StartDate = priceDTO.StartDate,
                EndDate = priceDTO.EndDate
            };

            context.SubscriptionPrices.Add(price);
            context.SaveChanges();
        }

        public List<SubscriptionPrice> GetAll()
        {
            return context.SubscriptionPrices
               .Include(p => p.Type)
               .ToList();
        }

        public SubscriptionPrice GetById(int priceId)
        {
            var price = context.SubscriptionPrices
                .FirstOrDefault(p => p.Id == priceId);

            if (price == null)
            {
                throw new SubscriptionPriceNotFoundException(priceId);
            }
            return price;
        }

        public void Update(int priceId, SubPriceUpdateRequestDTO priceDTO)
        {
            var price = context.SubscriptionPrices
                .FirstOrDefault(p => p.Id == priceId);

            if (price == null)
            {
                throw new SubscriptionPriceNotFoundException(priceId);
            }

            if (priceDTO.TypeId.HasValue)
            {
                var type = context.SubscriptionTypes
                    .FirstOrDefault(t => t.Id == priceDTO.TypeId);
                if (type == null)
                {
                    throw new SubscriptionTypeNotFoundException(priceDTO.TypeId.Value);
                }

                price.Type = type;
            }

            if (priceDTO.Price.HasValue)
                price.Price = priceDTO.Price.Value;

            if (priceDTO.StartDate.HasValue)
                price.StartDate = priceDTO.StartDate.Value;

            if (priceDTO.EndDate.HasValue)
                price.EndDate = priceDTO.EndDate.Value;

            context.SaveChanges();
        }
    }
}
