using Domain;
using MediatR;

namespace Application.Queries;

public class GetCryptoByUserId : IRequest<List<CryptoCurrency>>
{
    public string UserId { get; set; }
}
