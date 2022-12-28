using Domain;
using MediatR;

namespace Application.Commands
{
    public class DeleteUser : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
