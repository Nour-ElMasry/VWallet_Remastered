using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers;

public class GetCreditCardByIdHandler : IRequestHandler<GetCreditCardById, CreditCard>
{
    private readonly UserManager<User> _userManager;

    public GetCreditCardByIdHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreditCard> Handle(GetCreditCardById request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CreditCards)
            .ThenInclude(c => c.Transactions)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return null;

        var cc = await Task.Run(() => user.CreditCards.FirstOrDefault(cc => cc.CreditCardId == request.CreditCardId));

        if (cc == null)
            return null;

        return cc;
    }
}
