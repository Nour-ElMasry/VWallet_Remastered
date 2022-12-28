using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, User>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return null;

            await _userManager.DeleteAsync(user);

            return user;
        }
    }
}
