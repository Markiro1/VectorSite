namespace VectorSite.BL.Models
{
    public class LiqPayData
    {
        public int Version { get; set; } = 3;

        public string PublicKey { get; set; } = string.Empty;

        public string PrivateKey { get; set; } = string.Empty;

        public string Action {  get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; }  = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int OrderId { get; set; }
    }
}
