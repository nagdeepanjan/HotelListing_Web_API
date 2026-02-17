using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Data;

public class Country
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }


    [Required]
    public string ShortName { get; set; }

    
    //----------------------------------------------------
    public IList<Hotel> Hotels { get; set; } = [];
}