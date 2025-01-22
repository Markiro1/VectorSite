using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionTypeServiceDTO.Request
{
    public class SubTypeUpdateRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
    }
}
