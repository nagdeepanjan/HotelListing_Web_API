using HotelListing.API.DTOs.Country;

namespace HotelListing.API.Contracts
{
    public interface ICountriesService
    {
        Task<IEnumerable<GetCountriesDto>> GetCountriesAsync();

        Task<GetCountryDto?> GetCountryAsync(int id);

        Task UpdateCountryAsync(int id, UpdateCountryDto updateDto);

        Task<GetCountryDto> CreateCountryAsync(CreateCountryDto countryDto);

        Task DeleteCountryAsync(int id);
    }
}
