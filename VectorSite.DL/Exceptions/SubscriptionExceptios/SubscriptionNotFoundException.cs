namespace VectorSite.DL.Exceptions.SubscriptionExceptios
{
    public class SubscriptionNotFoundException : Exception
    {
        public SubscriptionNotFoundException(int subId)
            : base($"Subscription not found id: {subId}")
        { }

        public SubscriptionNotFoundException(string userId)
            : base($"Subscription not found by user id: {userId}")
        { }
    }
}
