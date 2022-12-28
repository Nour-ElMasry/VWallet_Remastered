using Domain;
using MediatR;

namespace Application.Commands;

public class CreateTransaction : IRequest<Transaction>
{
    public string SendingCCIban { get; set; }
    public string ReceivingCCIban { get; set; }
    public long Amount { get; set; }
}
