using MediatR;

namespace Application.Commands;

public class LoginUser : IRequest<Object>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
