using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionTypeControllerDTO
{
    public class SubscriptionTypeUpdateDTO
    {
        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
    }
}
