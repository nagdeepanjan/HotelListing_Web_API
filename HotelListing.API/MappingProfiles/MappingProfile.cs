using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Country;
using HotelListing.API.DTOs.Hotel;

namespace HotelListing.API.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<Hotel, GetHotelDto>()
            .ForCtorParam("Country", cfg => cfg.MapFrom(source => source.Country!.Name));
        //.ForMember(destination => destination.Country, cfg => cfg.MapFrom(source => source.Country!.Name));
        CreateMap<Hotel, GetHotelSlimDto>();
        CreateMap<CreateHotelDto, Hotel>();
    }
}

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<CreateCountryDto, Country>();
    }
}