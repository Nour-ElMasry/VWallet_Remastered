using Domain;
using MediatR;

namespace Application.Queries;

public class GetAllUsers : IRequest<List<User>>
{
}
