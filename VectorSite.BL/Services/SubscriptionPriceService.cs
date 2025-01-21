using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionPriceExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionPriceService(
        IDbContext context
    ) : ISubscriptionPriceService
    {
        public void Create(SubscriptionPriceCreateDTO priceDTO)
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
    }
}
