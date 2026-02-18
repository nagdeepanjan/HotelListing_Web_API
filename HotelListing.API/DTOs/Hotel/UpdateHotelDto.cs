using Microsoft.Build.Framework;

namespace HotelListing.API.DTOs.Hotel
{
    public class UpdateHotelDto:CreateHotelDto
    {
        [Required] public int Id { get; set; }
    }
}
