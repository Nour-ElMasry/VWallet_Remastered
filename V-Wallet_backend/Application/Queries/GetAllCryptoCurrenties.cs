using Domain;
using MediatR;

namespace Application.Queries;

public class GetAllCryptoCurrenties : IRequest<List<CryptoCurrency>>
{
}
