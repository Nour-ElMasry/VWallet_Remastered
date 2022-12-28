using Domain;
using MediatR;

namespace Application.Commands;

public class CashOutCrypto : IRequest<CryptoCurrency>
{
    public string UserId { get; set; }
    public long CryptoId { get; set; }
    public long CurrentValue { get; set; }
}
