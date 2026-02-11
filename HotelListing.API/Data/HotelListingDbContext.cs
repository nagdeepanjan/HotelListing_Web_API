using HotelListing.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : DbContext(options)
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels{ get; set; }
    }
}
