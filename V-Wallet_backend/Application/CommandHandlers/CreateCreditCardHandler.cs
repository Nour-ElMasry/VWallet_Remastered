using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;

namespace Application.CommandHandlers;

public class CreateCreditCardHandler : IRequestHandler<CreateCreditCard, CreditCard>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCreditCardHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreditCard> Handle(CreateCreditCard request, CancellationToken cancellationToken)
    {
        var cc = new CreditCard();

        await _unitOfWork.CreditCardRepository.AddCreditCard(cc);
        await _unitOfWork.Save();

        return cc;
    }
}
