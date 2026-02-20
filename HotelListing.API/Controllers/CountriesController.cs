using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Country;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesService countriesService) : ControllerBase
{

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await countriesService.GetCountriesAsync();
        
        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await countriesService.GetCountryAsync(id);

        if (country == null)
            return NotFound();

        return country;
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateDto)
    {
        if (id != updateDto.Id)
            return BadRequest();

        await countriesService.UpdateCountryAsync(id, updateDto);
        return NoContent();
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto countryDto)
    {
        var country = await countriesService.CreateCountryAsync(countryDto);

        return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await countriesService.DeleteCountryAsync(id);

        return NoContent();
    }

    
}