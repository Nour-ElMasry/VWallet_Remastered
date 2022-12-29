using MediatR;

namespace Application.Commands;

public class CheckUniqueUsername : IRequest<bool>
{
    public string Username { get; set; }
}
