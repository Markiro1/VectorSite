using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request
{
    public class SubTypeUpdateRequestDTO
    {
        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
    }
}
