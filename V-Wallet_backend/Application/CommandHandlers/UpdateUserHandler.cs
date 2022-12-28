using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers;

public class UpdateUserHandler : IRequestHandler<UpdateUser, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public UpdateUserHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(u => u.UserAddress).FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            return null;

        user.Name = request.Name;
        user.DateOfBirth = request.DateOfBirth;
        user.UserAddress.Country = request.Country;
        user.UserAddress.City = request.City;
        user.UserAddress.Street = request.Street;

        return user;
    }
}
