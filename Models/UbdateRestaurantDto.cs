using System.ComponentModel.DataAnnotations;

namespace RA.Models
{
    public class UbdateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public bool HasDelivery { get; set; }
    }
}
