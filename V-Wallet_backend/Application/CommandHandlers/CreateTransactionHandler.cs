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

        if (sendingCC == receivingCC)
            return null; 

        if (sendingCC.Deposit <= request.Amount)
            throw new ArgumentException("Insufficient Funds");

        sendingCC.Deposit -= request.Amount;
        receivingCC.Deposit += request.Amount;

        var tranSend = new Transaction(-request.Amount, sendingCC);
        
        sendingCC.Transactions.Add(tranSend);

        var tranReceive = new Transaction(request.Amount, receivingCC);

        receivingCC.Transactions.Add(tranReceive);

        await _unitOfWork.Save();

        return tranSend;
    }
}
