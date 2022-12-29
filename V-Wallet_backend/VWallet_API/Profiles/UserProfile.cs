using Application.Commands;
using Application.Dto;
using AutoMapper;
using Domain;
using VWallet_API.Dtos.UserDtos;

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

        CreateMap<UserPostDto, CreateUser>()
               .ForMember(cu => cu.Name, opt => opt.MapFrom(ud => ud.Name))
               .ForMember(cu => cu.DateOfBirth, opt => opt.MapFrom(ud => ud.DateOfBirth))
               .ForMember(cu => cu.Country, opt => opt.MapFrom(ud => ud.Country))
               .ForMember(cu => cu.City, opt => opt.MapFrom(ud => ud.City))
               .ForMember(cu => cu.Street, opt => opt.MapFrom(ud => ud.Street))
               .ForMember(cu => cu.Username, opt => opt.MapFrom(ud => ud.Username))
               .ForMember(cu => cu.Password, opt => opt.MapFrom(ud => ud.Password));

        CreateMap<UserPostDto, CreateAdmin>()
            .ForMember(cu => cu.Name, opt => opt.MapFrom(ud => ud.Name))
            .ForMember(cu => cu.DateOfBirth, opt => opt.MapFrom(ud => ud.DateOfBirth))
            .ForMember(cu => cu.Country, opt => opt.MapFrom(ud => ud.Country))
            .ForMember(cu => cu.City, opt => opt.MapFrom(ud => ud.City))
            .ForMember(cu => cu.Street, opt => opt.MapFrom(ud => ud.Street))
            .ForMember(cu => cu.Username, opt => opt.MapFrom(ud => ud.Username))
            .ForMember(cu => cu.Password, opt => opt.MapFrom(ud => ud.Password));

        CreateMap<UserPutDto, UpdateUser>()
            .ForMember(cu => cu.Name, opt => opt.MapFrom(ud => ud.Name))
            .ForMember(cu => cu.DateOfBirth, opt => opt.MapFrom(ud => ud.DateOfBirth))
            .ForMember(cu => cu.Country, opt => opt.MapFrom(ud => ud.Country))
            .ForMember(cu => cu.City, opt => opt.MapFrom(ud => ud.City))
            .ForMember(cu => cu.Street, opt => opt.MapFrom(ud => ud.Street));

    }
}
