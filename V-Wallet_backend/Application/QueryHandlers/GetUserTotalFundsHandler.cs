using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers;

public class GetUserTotalFundsHandler : IRequestHandler<GetUserTotalFunds, long>
{
    private readonly UserManager<User> _userManager;

    public GetUserTotalFundsHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<long> Handle(GetUserTotalFunds request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CreditCards)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            return 0;

        long totalFunds = 0;

        foreach (var cc in user.CreditCards)
        {
            totalFunds += totalFunds;
        }

        return totalFunds;
    }
}
