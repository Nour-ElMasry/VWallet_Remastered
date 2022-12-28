using Domain;
using MediatR;

namespace Application.Queries;

public class GetAllCreditCards : IRequest<List<CreditCard>>
{
}
