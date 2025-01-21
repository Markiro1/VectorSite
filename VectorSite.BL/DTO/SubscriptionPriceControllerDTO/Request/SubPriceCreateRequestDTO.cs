using System.ComponentModel.DataAnnotations;

namespace VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request
{
    public class SubPriceCreateRequestDTO
    {
        [Required(ErrorMessage = "Type id is required")]
        public int SubTypeId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
    }
}
