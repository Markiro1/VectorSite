using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using VectorSite.BL.DTO.CheckoutServiceDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.BL.Models;
using VectorSite.DL;
using VectorSite.DL.Exceptions.CheckoutExceptions;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class CheckoutService(
        IDbContext context,
        IMapper mapper,
        IConfiguration configuration
    ): ICheckoutService
    {
        public CheckoutSimpleDTO CreateCheckout(int subTypeId, string userId)
        {
            User? user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null) 
            {
                throw new UserNotFoundException(userId);
            }

            string subName = context.SubscriptionTypes.FirstOrDefault(t => t.Id == subTypeId)?
                .Name ?? string.Empty;

            if (string.IsNullOrEmpty(subName))
            {
                throw new SubscriptionTypeNotFoundException(subTypeId);
            }

            decimal price = context.SubscriptionPrices
                .Include(p => p.Type)
                .Where(p => p.Type.Id == subTypeId)
                .Where(p => p.StartDate <= DateTime.UtcNow && p.EndDate > DateTime.UtcNow)
                .FirstOrDefault()?.Price ?? decimal.Zero;

            if (price == 0)
            {
                throw new ArgumentException("Price cannot be zero");
            }

            string description = $"Користувач:{user.UserName}; Оплата за: {subName}; Сума: {price}; Дата оплати: {DateTime.UtcNow};";

            LiqPayData payData = new LiqPayData
            {
                OrderId = subTypeId,
                Amount = price,
                Currency = configuration["LiqPay:Currency"]!,
                Description = description,
                Action = configuration["LiqPay:Action"]!,
                PublicKey = configuration["LiqPay:PublicKey"]!,
                PrivateKey = configuration["LiqPay:PrivateKey"]!
            };

            string jsonString = JsonSerializer.Serialize(payData);

            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            string data = Convert.ToBase64String(jsonBytes);

            string rawSignature = configuration["LiqPay:PrivateKey"]! + data + configuration["LiqPay:PrivateKey"]!;

            string signature = string.Empty;
            
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(rawSignature));

                signature = Convert.ToBase64String(hashBytes);
            }

            if (string.IsNullOrEmpty(signature))
            {
                throw new ArgumentException("Signature cannot be empty or null");
            }

            CheckoutSimpleDTO response = new CheckoutSimpleDTO
            {
                Data = data,
                Signature = signature,
            };

            Checkout checkout = new Checkout
            {
                Amount = price,
                Status = CheckoutStatuses.Waiting,
                User = user
            };

            context.Checkouts.Add(checkout);
            context.SaveChanges();

            return response;
        }

        public List<CheckoutDTO> GetAll()
        {
            return context.Checkouts.
                Include(c => c.User)
                .ProjectTo<CheckoutDTO>(mapper.ConfigurationProvider)
                .ToList();
        }

        public CheckoutDTO GetById(int checkoutId)
        {
            Checkout? checkout = context.Checkouts
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == checkoutId);

            if (checkout == null)
            {
                throw new CheckoutNotFoundException(checkoutId);
            }

            return mapper.Map<CheckoutDTO>(checkout);
        }

    }
}
