using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers;

public class DeleteUserCreditCardHandler : IRequestHandler<DeleteUserCreditCard, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public DeleteUserCreditCardHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<User> Handle(DeleteUserCreditCard request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CreditCards)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        var cc = await _unitOfWork.CreditCardRepository.GetCreditCardById(request.CreditCardId);

        if (user == null || cc == null)
            return null;

        if (!user.CreditCards.Contains(cc))
            return null;

        user.CreditCards.Remove(cc);
        await _unitOfWork.Save();
        return user;
    }
}
