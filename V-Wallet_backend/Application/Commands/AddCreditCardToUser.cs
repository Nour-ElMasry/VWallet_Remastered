using Domain;
using MediatR;

namespace Application.Commands;

public class AddCreditCardToUser : IRequest<User>
{
    public String UserId { get; set; }
    public String Iban { get; set; }
    public DateTime ExpirtationDate { get; set; }
    public long Cvv { get; set; }
}
