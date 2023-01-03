using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<User>>
{
    private readonly UserManager<User> _userManager;

    public GetAllUsersHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.Include(u => u.UserAddress).ToListAsync(cancellationToken: cancellationToken);

        return users;
    }
}
