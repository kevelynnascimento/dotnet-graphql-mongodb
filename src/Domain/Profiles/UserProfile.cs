using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Domain.Dtos.Responses.User;
using Domain.Entities;

namespace Domain.Profiles;

[ExcludeFromCodeCoverage]
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserCreationResponse>()
            .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id))
            .ForMember(destination => destination.Name, map => map.MapFrom(source => source.Name))
            .ReverseMap();

        CreateMap<User, UserFilteringResponse>()
            .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id))
            .ForMember(destination => destination.Name, map => map.MapFrom(source => source.Name))
            .ReverseMap();

        CreateMap<User, UserFindByIdResponse>()
            .ForMember(destination => destination.Name, map => map.MapFrom(source => source.Name))
            .ReverseMap();

        CreateMap<User, UserUpdateResponse>()
            .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id))
            .ForMember(destination => destination.Name, map => map.MapFrom(source => source.Name))
            .ReverseMap();
    }
}
