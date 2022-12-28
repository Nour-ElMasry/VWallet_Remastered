using Domain;
using MediatR;

namespace Application.Commands;

public class DeleteUserCreditCard : IRequest<User>
{
    public string UserId { get; set; }
    public long CreditCardId { get; set; }
}
