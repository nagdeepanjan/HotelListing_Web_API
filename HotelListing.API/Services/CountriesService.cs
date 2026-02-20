using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Services;

public class CountriesService(HotelListingDbContext context): ICountriesService
{
    public async Task<IEnumerable<GetCountriesDto>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .Select(c => new GetCountriesDto(c.Id, c.Name, c.ShortName))
            .ToListAsync();

        return countries;
    }

    public async Task<GetCountryDto?> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(c => c.Id == id)
            .Select(c => new GetCountryDto(c.Id, c.Name, c.ShortName, c.Hotels.Select(h => new DTOs.Hotel.GetHotelSlimDto(h.Id, h.Name, h.Address, h.Rating)).ToList()))
            .FirstOrDefaultAsync();

        return country;
    }

    public async Task UpdateCountryAsync(int id, UpdateCountryDto updateDto)
    {
        var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");
        
        country.Name = updateDto.Name;
        country.ShortName = updateDto.ShortName;
        
        context.Countries.Update(country);
        await context.SaveChangesAsync();
    }

    public async Task<GetCountryDto> CreateCountryAsync(CreateCountryDto countryDto)
    {
        var country = new Country { Name = countryDto.Name, ShortName = countryDto.ShortName };

        context.Countries.Add(country);
        await context.SaveChangesAsync();

        var resultDto = new GetCountryDto(country.Id, country.Name, country.ShortName, []);

        return resultDto;
    }

    public async Task DeleteCountryAsync(int id)
    {
        var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country Not Found");
        

        context.Countries.Remove(country);
        await context.SaveChangesAsync();
    }

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.Id == id);
    }


}