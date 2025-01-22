namespace VectorSite.DL.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public Subscription Subscription { get; set; } = null!;

        public decimal Price { get; set; } = decimal.Zero;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
