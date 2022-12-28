using Domain;
using MediatR;

namespace Application.Commands;

public class UpdateUser : IRequest<User>
{
    public String UserId { get; set; }
    public String Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public String Country { get; set; }
    public String City { get; set; }
    public String Street { get; set; }
}
