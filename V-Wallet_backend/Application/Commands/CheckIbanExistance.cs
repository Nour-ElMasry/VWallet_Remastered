using MediatR;

namespace Application.Commands;

public class CheckIbanExistance : IRequest<bool>
{
    public string Iban{ get; set; }
}
