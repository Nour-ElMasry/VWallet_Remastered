using Domain;
using MediatR;

namespace Application.Commands;

public class CreateCryptoInvestment : IRequest<CryptoCurrency>
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public long Value { get; set; }
    public long Investment { get; set; }
}
