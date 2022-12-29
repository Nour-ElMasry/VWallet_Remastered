using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers;

public class CheckIbanExistanceHanlder : IRequestHandler<CheckIbanExistance, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckIbanExistanceHanlder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CheckIbanExistance request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CreditCardRepository.GetCreditCardByIban(request.Iban) != null;
    }
}
