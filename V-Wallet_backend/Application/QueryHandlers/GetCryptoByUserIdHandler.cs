using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers;

public class GetCryptoByUserIdHandler : IRequestHandler<GetCryptoByUserId, List<CryptoCurrency>>
{
    private readonly UserManager<User> _userManager;

    public GetCryptoByUserIdHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<CryptoCurrency>> Handle(GetCryptoByUserId request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CryptoCurrencies)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return null;

        return user.CryptoCurrencies;
    }
}
