using Application.Commands;
using AutoMapper;
using Domain;
using VWallet_API.Dtos.TransactionDtos;

namespace VWallet_API.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionPostDto, CreateTransaction>()
            .ForMember(ud => ud.SendingCCIban, opt => opt.MapFrom(u => u.SendingCCIban))
            .ForMember(ud => ud.ReceivingCCIban, opt => opt.MapFrom(u => u.ReceivingCCIban))
            .ForMember(ud => ud.Amount, opt => opt.MapFrom(u => u.Amount));
    }
}
