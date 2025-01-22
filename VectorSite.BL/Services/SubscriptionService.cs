using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionControllerDTO.Response;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionExceptios;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionService(
        IUserService userService,
        IDbContext context,
        IMapper mapper
    ) : ISubscriptionService
    {
        // TODO: Додати логіку в майбутньому, щоб виключати останню підписку (IsCancelled)
        public void Create(int subTypeId, string userId)
        {
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var subType = context.SubscriptionTypes.FirstOrDefault(t => t.Id == subTypeId);
            if (subType == null)
            {
                throw new SubscriptionTypeNotFoundException(subTypeId);
            }

            var sub = new Subscription()
            {
                User = user,
                SubType = subType
            };

            context.Subscriptions.Add(sub);
            context.SaveChanges();
        }


        public List<SubResponseDTO> GetAllSubs()
        {
            List<SubResponseDTO> subDTOs = context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.SubType)
                .ProjectTo<SubResponseDTO>(mapper.ConfigurationProvider)
                .ToList() ?? new();

            return subDTOs;
        }

        public SubWithDetailsResponseDTO GetByUserId(string userId)
        {
            var currSub = context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.SubType)
                    .ThenInclude(t => t.Prices)
                //.Where(s => !s.IsCancelled) // Uncomment in future
                .Where(s => s.User.Id == userId)
                .Where(s => DateTime.Now.ToUniversalTime() > s.StartDate && DateTime.Now.ToUniversalTime() < s.EndDate)
                .FirstOrDefault(s => s.User.Id == userId);

            if (currSub == null)
            {
                throw new SubscriptionNotFoundException(userId);
            }

            SubWithDetailsResponseDTO subWithDetails = new()
            {
                StartDate = currSub.StartDate,
                EndDate = currSub.EndDate,
                IsCancelled = currSub.IsCancelled,
                IsPayed = currSub.Payment != null,
                Price = 0 // TODO Price
            };

            return subWithDetails;
        }

        //TODO: Змінити це чи взагалі видалити, бо херня (Та й нахер треба)
        public void Update(int subId, SubUpdateRequestDTO updateDTO)
        {
            var sub = context.Subscriptions
                .Include(s => s.Payment)
                .FirstOrDefault(s => s.Id == subId);

            if (sub == null)
            {
                throw new SubscriptionNotFoundException(subId);
            }

            if (updateDTO.SubTypeId.HasValue)
                sub.SubTypeId = updateDTO.SubTypeId.Value;

            if (updateDTO.IsCancelled.HasValue)
                sub.IsCancelled = updateDTO.IsCancelled.Value;

            // TODO Payment
            if (sub.Payment != null)
            {
                if (updateDTO.IsPayed.HasValue)
                {
                    sub.Payment.Status = "PAID";
                }
                else
                {
                    sub.Payment.Status = "UNPAID";
                }
            }

            if (updateDTO.StartDate.HasValue)
                sub.StartDate = updateDTO.StartDate.Value;

            if (updateDTO.EndDate.HasValue)
                sub.EndDate = updateDTO.EndDate.Value;

            context.SaveChanges();
        }
    }
}
