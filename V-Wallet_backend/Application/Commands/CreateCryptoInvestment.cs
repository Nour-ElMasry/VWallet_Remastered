using Domain;
using MediatR;

namespace Application.Commands;

public class CreateCryptoInvestment : IRequest<CryptoCurrency>
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public decimal Investment { get; set; }
}
