using VectorSite.Interfaces.Repositories;
using VectorSite.Models;

namespace VectorSite.Repositories
{
    public class SubscriptionTypeRepository : ISubscriptionTypeRepository
    {
        private readonly NpgsqlDbContext context;

        public SubscriptionTypeRepository(NpgsqlDbContext context)
        {
            this.context = context;
        }

        public SubscriptionType GetTypeByName(string name)
        {
            return context.SubscriptionTypes
                .First(type => type.Name == name);
        }
    }
}
