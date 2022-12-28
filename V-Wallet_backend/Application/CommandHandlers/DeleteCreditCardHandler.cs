using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;

namespace Application.CommandHandlers;

public class DeleteCreditCardHandler : IRequestHandler<DeleteCreditCard, CreditCard>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCreditCardHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreditCard> Handle(DeleteCreditCard request, CancellationToken cancellationToken)
    {
        var cc = await _unitOfWork.CreditCardRepository.GetCreditCardById(request.CreditCardId);

        if (cc == null)
            return null;

        await _unitOfWork.CreditCardRepository.DeleteCreditCard(cc);
        await _unitOfWork.Save();

        return cc;
    }
}
