using Domain;
using MediatR;

namespace Application.Commands;

public class AddCreditCardToUser : IRequest<User>
{
    public int UserId { get; set; }
    public String Iban { get; set; }
    public DateOnly ExpirtationDate { get; set; }
    public long Cvv { get; set; }
}
