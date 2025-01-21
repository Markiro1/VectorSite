namespace VectorSite.DL.Exceptions.SubscriptionTypeExceptions
{
    public class SubscriptionTypeNotFoundException : Exception
    {
        public SubscriptionTypeNotFoundException(int id)
            :base($"Subscription type not found by id: {id}")
        { }

        public SubscriptionTypeNotFoundException(string name)
            : base($"Subscription type not found by name: {name}")
        { }
    }
}
