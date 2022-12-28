using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers;

public class GetCreditCardsByUserIdHandler : IRequestHandler<GetCreditCardsByUserId, List<CreditCard>>
{
    private readonly UserManager<User> _userManager;

    public GetCreditCardsByUserIdHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<CreditCard>> Handle(GetCreditCardsByUserId request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CreditCards)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return null;

        return user.CreditCards;
    }
}
