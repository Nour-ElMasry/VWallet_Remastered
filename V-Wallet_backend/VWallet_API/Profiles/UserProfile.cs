using AutoMapper;
using Domain;
using VWallet_API.Dtos;

namespace VWallet_API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserGetDto>()
            .ForMember(ud => ud.Id, opt => opt.MapFrom(u => u.Id))
            .ForMember(ud => ud.Username, opt => opt.MapFrom(u => u.UserName))
            .ForMember(ud => ud.Name, opt => opt.MapFrom(u => u.Name))
            .ForMember(ud => ud.DateOfBirth, opt => opt.MapFrom(u => u.DateOfBirth))
            .ForMember(ud => ud.UserAddress, opt => opt.MapFrom(u => u.UserAddress));

    }
}
