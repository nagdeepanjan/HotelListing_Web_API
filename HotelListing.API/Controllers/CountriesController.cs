using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Country;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(HotelListingDbContext context) : ControllerBase
{

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await context.Countries
            .Select(c=>new GetCountriesDto(c.Id, c.Name, c.ShortName))
            .ToListAsync();
        
        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await context.Countries
            .Where(c=>c.Id == id)
            .Select(c=>new GetCountryDto(c.Id, c.Name, c.ShortName, c.Hotels.Select(h=>new DTOs.Hotel.GetHotelsDto(h.Id, h.Name, h.Address, h.Rating,h.CountryId)).ToList()))
            .FirstOrDefaultAsync();

        if (country == null)
        {
            return NotFound();
        }

        return country;
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateDto)
    {
        if (id != updateDto.Id)
            return BadRequest();

        var country = await context.Countries.FindAsync(id);
        if (country is null)
            return NotFound();

        country.Name = updateDto.Name;
        country.ShortName = updateDto.ShortName;

        context.Entry(country).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await CountryExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto countryDto)
    {
        var country = new Country { Name = countryDto.Name, ShortName = countryDto.ShortName };
        
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        var resultDto = new GetCountryDto(country.Id, country.Name, country.ShortName, []);

        return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, resultDto);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        context.Countries.Remove(country);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.Id == id);
    }
}