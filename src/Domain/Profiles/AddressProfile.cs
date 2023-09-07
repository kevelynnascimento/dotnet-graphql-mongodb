using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Domain.Dtos.Responses.Address;
using Domain.Models;

namespace Domain.Profiles;

[ExcludeFromCodeCoverage]
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressFindByPostalCodeModel, AddressFindByPostalCodeResponse>()
            .ForMember(destination => destination.PostalCode, map => map.MapFrom(source => source.PostalCode))
            .ForMember(destination => destination.City, map => map.MapFrom(source => source.City))
            .ForMember(destination => destination.Country, map => map.MapFrom(source => source.Country))
            .ForMember(destination => destination.Street, map => map.MapFrom(source => source.Street))
            .ForMember(destination => destination.Number, map => map.MapFrom(source => source.Number))
            .ForMember(destination => destination.Neighborhood, map => map.MapFrom(source => source.Neighborhood))
            .ForMember(destination => destination.Complement, map => map.MapFrom(source => source.Complement))
            .ForMember(destination => destination.Reference, map => map.MapFrom(source => source.Reference))
            .ForMember(destination => destination.GeoCoordinates, map => map.MapFrom(source => source.GeoCoordinates))
            .ReverseMap();
    }
}
