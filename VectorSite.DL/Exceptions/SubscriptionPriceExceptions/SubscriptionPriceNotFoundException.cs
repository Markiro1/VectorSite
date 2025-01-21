namespace VectorSite.DL.Exceptions.SubscriptionPriceExceptions
{
    public class SubscriptionPriceNotFoundException : Exception
    {
        public SubscriptionPriceNotFoundException(int priceId) 
            : base($"Subscription price not found by id: {priceId}")
        { }
    }
}
