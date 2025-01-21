using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionControllerDTO.Request;
using VectorSite.BL.DTO.SubscriptionControllerDTO.Response;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Response;
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
        ISubscriptionTypeService subscriptionTypeService,
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
            List<Subscription> subs = context?.Subscriptions
                .Include(s => s.SubType)
                .Include(s => s.User)
                .ToList() ?? [];

            List<SubResponseDTO> subDTOs = subs.Select(sub => new SubResponseDTO
            {
                TypeId = sub.SubType.Id,
                UserId = sub.User.Id,
                IsCancelled = sub.IsCancelled,
                IsPayed = sub.IsPayed,
                StartDate = sub.StartDate,
                EndDate = sub.EndDate,
            }).ToList();

            return subDTOs;
        }

        public SubWithDetailsResponseDTO GetByUserId(string userId)
        {
            var sub = context.Subscriptions
                .Include(s => s.SubType)
                    .ThenInclude(t => t.Payments)
                .Include(s => s.SubType)
                    .ThenInclude(p => p.Prices)
                .FirstOrDefault(s => s.User.Id.Equals(userId));
            if (sub == null)
            {
                throw new SubscriptionNotFoundException(userId);
            }

            SubTypeResponseDTO subType = mapper.Map<SubTypeResponseDTO>(sub.SubType);

            SubWithDetailsResponseDTO subWithDetails = mapper.Map<SubWithDetailsResponseDTO>(sub);
            subWithDetails.SubType = subType;

            return subWithDetails;
        }

        //TODO: Змінити це чи взагалі видалити, бо херня (Та й нахер треба)
        public void Update(int subId, SubUpdateRequestDTO updateDTO)
        {
            var sub = context.Subscriptions
                .Include(s => s.SubType)
                .FirstOrDefault(s => s.Id == subId);

            if (sub == null)
            {
                throw new SubscriptionNotFoundException(subId);
            }

            if (updateDTO.IsCancelled.HasValue)
                sub.IsCancelled = updateDTO.IsCancelled.Value;

            if (updateDTO.IsPayed.HasValue)
                sub.IsPayed = updateDTO.IsPayed.Value;

            if (updateDTO.StartDate.HasValue)
                sub.StartDate = updateDTO.StartDate.Value;

            if (updateDTO.EndDate.HasValue)
                sub.EndDate = updateDTO.EndDate.Value;

            context.SaveChanges();
        }
    }
}
