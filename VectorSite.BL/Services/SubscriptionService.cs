using System;
using VectorSite.BL.DTO.SubscriptionControllerDTO;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;
using VectorSite.DL.Exceptions.UserExceptions;
using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class SubscriptionService(
        IUserService userService,
        ISubscriptionTypeService subscriptionTypeService,
        ISubscriptionRepository subscriptionRepository,
        IDbContext context
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

            var subType = subscriptionTypeService.GetTypeById(subTypeId);
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


        public List<SubscriptionDTO> GetAllSubs()
        {
            List<Subscription> subs = subscriptionRepository.GetAllSubs();

            List<SubscriptionDTO> subDTOs = subs.Select(sub => new SubscriptionDTO
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

        public SubscriptionWithDetailsDTO GetSubscriptionByUserId(string userId)
        {
            var sub = subscriptionRepository.GetSubscriptionByUserId(userId);

            var subType = new SubscriptionTypeDTO
            {
                Id = sub.SubType.Id,
                Name = sub.SubType.Name,
                Days = sub.SubType.Days,
                Payments = sub.SubType.Payments,
                Prices = sub.SubType.Prices
            };

            return new SubscriptionWithDetailsDTO
            {
                SubType = subType,
                IsCancelled = sub.IsCancelled,
                IsPayed = sub.IsPayed,
                StartDate = sub.StartDate,
                EndDate = sub.EndDate,
            };
        }

        //TODO: Змінити це чи взагалі видалити, бо херня (Та й нахер треба)
        public void Update(int subId, SubscriptionUpdateDTO updateDTO)
        {
            using (var transaction = context.BeginTransaction())
            {
                try
                {       
                    var sub = subscriptionRepository.GetSubscriptionById(subId);

                    if (updateDTO.IsCancelled.HasValue)
                        sub.IsCancelled = updateDTO.IsCancelled.Value;

                    if (updateDTO.IsPayed.HasValue)
                        sub.IsPayed = updateDTO.IsPayed.Value;

                    if (updateDTO.StartDate.HasValue)
                        sub.StartDate = updateDTO.StartDate.Value;

                    if (updateDTO.EndDate.HasValue)
                        sub.EndDate = updateDTO.EndDate.Value;

                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error updating subscription: {ex.Message}");
                }
            }
        }
    }
}
