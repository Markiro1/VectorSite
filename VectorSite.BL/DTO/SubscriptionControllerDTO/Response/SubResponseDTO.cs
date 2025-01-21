using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;

namespace VectorSite.BL.DTO.SubscriptionControllerDTO.Response
{
    public class SubResponseDTO : IMapWith<Subscription>
    {
        public int TypeId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public bool IsCancelled { get; set; } = false;

        public bool IsPayed { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
