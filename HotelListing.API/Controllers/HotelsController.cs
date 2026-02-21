using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Hotel;
using HotelListing.API.Services;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(IHotelsService hotelsService) : ControllerBase
{

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
    {
        var hotels = await hotelsService.GetHotelsAsync();

        return Ok(hotels);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await hotelsService.GetHotelAsync(id);

        if (hotel == null) return NotFound();

        return Ok(hotel);
    }

    // PUT: api/Hotels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotelDto)
    {
        if (id != hotelDto.Id)
            return BadRequest();

        try
        {
            await hotelsService.UpdateHotelAsync(id, hotelDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await hotelsService.HotelExistsAsync(id))
                return NotFound();
            
            throw;
        }

        return NoContent();
    }

    // POST: api/Hotels
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
    {

        var hotel =await hotelsService.CreateHotelAsync(hotelDto);

        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await hotelsService.DeleteHotelAsync(id);

        return NoContent();
    }

    
}