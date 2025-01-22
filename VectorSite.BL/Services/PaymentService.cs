using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.PaymentServiceDTO.Response;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.CheckoutExceptions;
using VectorSite.DL.Exceptions.PaymentExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class PaymentService(
        IDbContext context,
        IMapper mapper
    ) : IPaymentService
    {
        public PaymentResponseDTO Create(int checkoutId)
        {
            // Get waiting checkout by id
            Checkout? waitingCheckout = context.Checkouts
                .Include(c => c.User)
                .Include(c => c.SubType)
                .Where(c => c.Status.ToUpper().Equals(CheckoutStatuses.Waiting))
                .FirstOrDefault(c => c.Id == checkoutId);

            if (waitingCheckout == null)
            {
                throw new CheckoutNotFoundException(checkoutId);
            }

            // Create Payment
            Payment payment = new Payment
            {
                Id = checkoutId, // TODO Через те, що зараз вже створені payments виникає помилка, что key is cannot be dublicate, тому що checkoutId = 1, а у нас в бд вже є payment з id = 1
                Price = waitingCheckout.Amount,
                Date = DateTime.UtcNow,
            };
            context.Payments.Add(payment);

            int subTypeDurationDays = waitingCheckout.SubType.Days;

            // Create subscription
            Subscription subscription = new Subscription
            {
                SubType = waitingCheckout.SubType,
                SubTypeId = waitingCheckout.SubType.Id,
                User = waitingCheckout.User,
                IsCancelled = false,
                Payment = payment,
                DateFrom = DateTime.UtcNow,
                DateTo = DateTime.UtcNow.AddDays(subTypeDurationDays)
            };
            context.Subscriptions.Add(subscription);

            // Set subscription to payment
            payment.Subscription = subscription;
            payment.SubscriptionId = subscription.Id;

            waitingCheckout.Status = CheckoutStatuses.Success;

            context.SaveChanges();

            return mapper.Map<PaymentResponseDTO>(payment);
        }

        public List<PaymentSimpleResponseDTO> GetAll()
        {
            return context.Payments
                .Include(p => p.Subscription)
                    .ThenInclude(s => s.User)
                .ProjectTo<PaymentSimpleResponseDTO>(mapper.ConfigurationProvider)
                .ToList();
        }

        public PaymentSimpleResponseDTO GetById(int paymentId)
        {
            Payment? payment = context.Payments
                .Include(p => p.Subscription)
                    .ThenInclude(s => s.User)
                .FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
            {
                throw new PaymentNotFoundException(paymentId);
            }

            return mapper.Map<PaymentSimpleResponseDTO>(payment);
        }
    }
}
