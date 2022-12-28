using Application.Abstract;
using Application.Queries;
using Domain;
using MediatR;

namespace Application.QueryHandlers;

public class GetAllCryptoCurrentiesHandler : IRequestHandler<GetAllCryptoCurrenties, List<CryptoCurrency>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCryptoCurrentiesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CryptoCurrency>> Handle(GetAllCryptoCurrenties request, CancellationToken cancellationToken)
    {
        var cryptos = await _unitOfWork.CryptoRepository.GetAllCryptoCurrencies();

        return cryptos;
    }
}
