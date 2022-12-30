using Application.Commands;
using AutoMapper;
using Domain;
using VWallet_API.Dtos.TransactionDtos;

namespace VWallet_API.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionGetDto>()
            .ForMember(ud => ud.TransactionId, opt => opt.MapFrom(u => u.TransactionId))
            .ForMember(ud => ud.DateOfTransaction, opt => opt.MapFrom(u => u.DateOfTransaction))
            .ForMember(ud => ud.Amount, opt => opt.MapFrom(u => u.Amount))
            .ForMember(ud => ud.TransactionIssuer, opt => opt.MapFrom(u => u.CC.Iban));

        CreateMap<TransactionPostDto, CreateTransaction>()
            .ForMember(ud => ud.SendingCCIban, opt => opt.MapFrom(u => u.SendingCCIban))
            .ForMember(ud => ud.ReceivingCCIban, opt => opt.MapFrom(u => u.ReceivingCCIban))
            .ForMember(ud => ud.Amount, opt => opt.MapFrom(u => u.Amount));
    }
}
