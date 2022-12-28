using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;

namespace Application.CommandHandlers;

public class CreateTransactionHandler : IRequestHandler<CreateTransaction, Transaction>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> Handle(CreateTransaction request, CancellationToken cancellationToken)
    {
        var sendingCC = await _unitOfWork.CreditCardRepository.GetCreditCardByIban(request.SendingCCIban);
        var receivingCC = await _unitOfWork.CreditCardRepository.GetCreditCardByIban(request.ReceivingCCIban);

        if (sendingCC == null || receivingCC == null)
            return null;

        if (sendingCC.Deposit <= request.Amount)
            throw new ArgumentException("Insufficient Funds");

        sendingCC.Deposit -= request.Amount;
        receivingCC.Deposit += request.Amount;

        var trans = new Transaction(request.Amount, receivingCC, sendingCC);
        
        sendingCC.Transactions.Add(trans);
        receivingCC.Transactions.Add(trans);

        await _unitOfWork.Save();

        return trans;
    }
}
