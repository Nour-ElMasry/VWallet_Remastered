using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers;

public class GetCreditCardTransactionsByIdHandler : IRequestHandler<GetCreditCardTransactionsById, List<Transaction>>
{
    private readonly UserManager<User> _userManager;

    public GetCreditCardTransactionsByIdHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<Transaction>> Handle(GetCreditCardTransactionsById request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(u => u.CreditCards).ThenInclude(cc => cc.Transactions).FirstOrDefaultAsync(u => u.Id == request.UserId);
        
        if(user == null)
            return null;

        var cc = await Task.Run(() => user.CreditCards.FirstOrDefault(cc => cc.CreditCardId == request.CreditCardId));

        if (cc == null)
            return null;

        return cc.Transactions;
    }
}
