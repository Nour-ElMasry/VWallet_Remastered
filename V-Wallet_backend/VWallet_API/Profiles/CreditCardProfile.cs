using AutoMapper;
using Domain;
using VWallet_API.Dtos.CreditCardDtos;
using VWallet_API.Dtos.TransactionDtos;
using VWallet_API.Dtos.UserDtos;

namespace VWallet_API
{
    public class CreditCardProfile : Profile
    {
        public CreditCardProfile()
        {
            CreateMap<CreditCard, CreditCardInfoGetDto>()
            .ForMember(ud => ud.CreditCardId, opt => opt.MapFrom(u => u.CreditCardId))
            .ForMember(ud => ud.Iban, opt => opt.MapFrom(u => u.Iban))
            .ForMember(ud => ud.ExpirtationDate, opt => opt.MapFrom(u => u.ExpirtationDate))
            .ForMember(ud => ud.Deposit, opt => opt.MapFrom(u => u.Deposit));

            CreateMap<CreditCard, CreditCardGetDto>()
            .ForMember(ud => ud.CreditCardId, opt => opt.MapFrom(u => u.CreditCardId))
            .ForMember(ud => ud.Iban, opt => opt.MapFrom(u => u.Iban))
            .ForMember(ud => ud.ExpirtationDate, opt => opt.MapFrom(u => u.ExpirtationDate))
            .ForMember(ud => ud.Deposit, opt => opt.MapFrom(u => u.Deposit))
            .ForMember(ud => ud.Transactions, opt => opt.MapFrom(u => u.Transactions.Select(t => 
                new TransactionGetDto
                {
                    TransactionId = t.TransactionId,
                    DateOfTransaction = t.DateOfTransaction,
                    Amount = t.Amount,
                    TransactionIssuer = t.CC.Iban
                }
            )));
        }
    }
}
