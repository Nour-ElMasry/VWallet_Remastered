using Domain;
using MediatR;

namespace Application.Queries;

public class GetCreditCardById : IRequest<CreditCard>
{
    public string UserId { get; set; }
    public long CreditCardId { get; set; }
}
