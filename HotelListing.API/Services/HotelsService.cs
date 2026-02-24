using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Services;

public class HotelsService(HotelListingDbContext context, IMapper mapper): IHotelsService
{
    public async Task<IEnumerable<GetHotelsDto>> GetHotelsAsync()
    {
        var hotels = await context.Hotels
            .Include(h=>h.Country)
            .Select(h => new GetHotelsDto(h.Id, h.Name, h.Address, h.Rating, h.CountryId))
            .ToListAsync();

        return hotels;
    }

    public async Task<GetHotelDto?> GetHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .Include(h => h.Country)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)                       
            .FirstOrDefaultAsync();

        return hotel ?? null;
    }

    public async Task UpdateHotelAsync(int id, UpdateHotelDto updateDto)
    {
        var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");

        hotel.Name = updateDto.Name;
        hotel.Address = updateDto.Address;
        hotel.Rating = updateDto.Rating;
        hotel.CountryId = updateDto.CountryId;

        context.Entry(hotel).State = EntityState.Modified;

        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();
    }

    public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto hotelDto)
    {
        //var hotel = new Hotel { Name = hotelDto.Name, Address = hotelDto.Address, Rating = hotelDto.Rating, CountryId = hotelDto.CountryId};
        var hotel = mapper.Map<Hotel>(hotelDto);

        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        var resultDto = new GetHotelDto(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, hotel.CountryId, string.Empty);

        return resultDto;
    }

    public async Task DeleteHotelAsync(int id)
    {
        var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel Not Found");
        

        context.Hotels.Remove(hotel);
        await context.SaveChangesAsync();
    }

    public async Task<bool> HotelExistsAsync(int id)
    {
        return await context.Hotels.AnyAsync(e => e.Id == id);
    }

}