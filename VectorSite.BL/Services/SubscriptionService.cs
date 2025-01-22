using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.SubscriptionServiceDTO.Request;
using VectorSite.BL.DTO.SubscriptionServiceDTO.Response;
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
                .Include(s => s.Payment)
                .Where(s => !s.IsCancelled)
                .Where(s => s.User.Id == userId)
                .Where(s => DateTime.UtcNow >= s.DateFrom && DateTime.UtcNow < s.DateTo)
                .Where(s => s.Payment != null)
                .FirstOrDefault(s => s.User.Id == userId);

            if (currSub == null)
            {
                throw new SubscriptionNotFoundException(userId);
            }

            return mapper.Map<SubWithDetailsResponseDTO>(currSub);
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

            if (updateDTO.StartDate.HasValue)
                sub.DateFrom = updateDTO.StartDate.Value;

            if (updateDTO.EndDate.HasValue)
                sub.DateTo = updateDTO.EndDate.Value;

            context.SaveChanges();
        }
    }
}
