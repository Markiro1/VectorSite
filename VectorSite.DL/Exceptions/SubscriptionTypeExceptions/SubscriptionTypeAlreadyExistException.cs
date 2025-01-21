namespace VectorSite.DL.Exceptions.SubscriptionTypeExceptions
{
    public class SubscriptionTypeAlreadyExistException : Exception
    {
        public SubscriptionTypeAlreadyExistException(string name)
            : base($"Subscription type already exists by name: {name}")
        { }

    }
}
