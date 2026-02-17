using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs.Country
{
    public class CreateCountryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string ShortName { get; set; }
    }
}
