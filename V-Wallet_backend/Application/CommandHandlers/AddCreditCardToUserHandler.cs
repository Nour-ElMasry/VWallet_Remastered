using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;

namespace Application.CommandHandlers;

public class AddCreditCardToUserHandler : IRequestHandler<AddCreditCardToUser, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCreditCardToUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(AddCreditCardToUser request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);
        var creditCard = await _unitOfWork.CreditCardRepository.GetCreditCard(request.Iban, request.ExpirtationDate, request.Cvv);

        if (creditCard == null || user == null)
            return null;

        user.CreditCards.Add(creditCard);
        await _unitOfWork.Save();

        return user;
    }
}
