using Application.Abstract;
using Application.Queries;
using Domain;
using MediatR;

namespace Application.QueryHandlers;

public class GetAllCreditCardsHandler : IRequestHandler<GetAllCreditCards, List<CreditCard>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCreditCardsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CreditCard>> Handle(GetAllCreditCards request, CancellationToken cancellationToken)
    {
        var CCs = await _unitOfWork.CreditCardRepository.GetAllCreditCards();

        return CCs;
    }
}
