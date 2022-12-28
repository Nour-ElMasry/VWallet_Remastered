using Domain;
using MediatR;

namespace Application.Queries;

public class GetCreditCardsByUserId : IRequest<List<CreditCard>>
{
    public string UserId { get; set; }
}
