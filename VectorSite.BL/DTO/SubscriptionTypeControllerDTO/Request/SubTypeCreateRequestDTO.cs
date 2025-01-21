using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request
{
    public class SubTypeCreateRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
    }
}
