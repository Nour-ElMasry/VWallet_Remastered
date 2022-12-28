using MediatR;

namespace Application.Commands;

public class CreateUser : IRequest<Object>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public String Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public String Country { get; set; }
    public String City { get; set; }
    public String Street { get; set; }
}
