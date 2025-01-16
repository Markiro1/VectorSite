using VectorSite.DL.Interfaces.Repositories;
using VectorSite.DL.Models;

namespace VectorSite.DL.Repositories
{
    public class SubscriptionTypeRepository : ISubscriptionTypeRepository
    {
        private readonly IDbContext context;

        public SubscriptionTypeRepository(IDbContext context)
        {
            this.context = context;
        }

        public SubscriptionType GetTypeById(int id)
        {
            return context.SubscriptionTypes
                .First(type => type.Id == id);
        }
    }
}
