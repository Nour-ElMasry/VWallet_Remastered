using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandHandlers;

public class CheckUniqueUsernameHandler : IRequestHandler<CheckUniqueUsername, bool>
{
    private readonly UserManager<User> _userManager;

    public CheckUniqueUsernameHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(CheckUniqueUsername request, CancellationToken cancellationToken)
    {
        return await _userManager.FindByNameAsync(request.Username) == null;
    }
}
