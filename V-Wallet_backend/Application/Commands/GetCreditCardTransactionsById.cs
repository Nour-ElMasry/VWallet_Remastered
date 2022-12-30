using Domain;
using MediatR;

namespace Application.Commands;

public class GetCreditCardTransactionsById : IRequest<List<Transaction>>
{
    public string UserId { get; set; }
    public long CreditCardId { get; set; }
}
