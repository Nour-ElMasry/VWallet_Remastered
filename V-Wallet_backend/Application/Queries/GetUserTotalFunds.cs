using MediatR;

namespace Application.Queries;

public class GetUserTotalFunds : IRequest<long>
{
    public string UserId { get; set; }
}
