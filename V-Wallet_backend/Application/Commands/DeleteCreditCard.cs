using Domain;
using MediatR;

namespace Application.Commands;

public class DeleteCreditCard : IRequest<CreditCard>
{
    public long CreditCardId { get; set; }
}
