using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers;

public class AddCreditCardToUserHandler : IRequestHandler<AddCreditCardToUser, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AddCreditCardToUserHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<User> Handle(AddCreditCardToUser request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        var creditCard = await _unitOfWork.CreditCardRepository.GetCreditCard(request.Iban, request.ExpirtationDate, request.Cvv);

        if (creditCard == null || user == null)
            return null;

        user.CreditCards.Add(creditCard);
        await _unitOfWork.Save();

        return user;
    }
}
