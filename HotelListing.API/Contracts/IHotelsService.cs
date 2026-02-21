using HotelListing.API.DTOs.Hotel;

namespace HotelListing.API.Contracts
{
    public interface IHotelsService
    {
        Task<IEnumerable<GetHotelsDto>> GetHotelsAsync();

        Task<GetHotelDto?> GetHotelAsync(int id);

        Task UpdateHotelAsync(int id, UpdateHotelDto updateDto);

        Task<GetHotelDto> CreateHotelAsync(CreateHotelDto countryDto);

        Task DeleteHotelAsync(int id);

        Task<bool> HotelExistsAsync(int id);
    }
}
