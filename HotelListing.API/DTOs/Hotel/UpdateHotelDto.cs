using Microsoft.Build.Framework;

namespace HotelListing.API.DTOs.Hotel
{
    public class UpdateHotelDto:CreateHotelDto
    {
        [Required] public string Id { get; set; }
    }
}
