namespace VectorSite.Models
{
    public class Payment
    {
        public string Id { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public DateTime Time { get; set; } = DateTime.Now;

        public SubscriptionType Type { get; set; } = null!;
    }
}
