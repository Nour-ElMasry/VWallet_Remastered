using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.CommandHandlers;

public class CreateCyptoInvestmentHandler : IRequestHandler<CreateCryptoInvestment, CryptoCurrency>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CreateCyptoInvestmentHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    public async Task<CryptoCurrency> Handle(CreateCryptoInvestment request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CreditCards)
            .Include(u => u.CryptoCurrencies)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return null;

        decimal totalDeposit = 0;
        var creditCards = user.CreditCards.OrderByDescending(cc => cc.Deposit);

        foreach (var cc in creditCards)
        {
            totalDeposit += cc.Deposit;
        }

        var investment = request.Investment;

        if (investment > totalDeposit)
            throw new ArgumentException("Insufficient funds");

        foreach (var cc in creditCards)
        {
            if (cc.Deposit >= investment)
            {
                cc.Deposit -= investment;
                cc.Transactions.Add(new Transaction(-investment, "Crypto Investment", request.Name));
                investment = 0;
                break;
            }
            else
            {
                investment -= cc.Deposit;
                cc.Transactions.Add(new Transaction(-cc.Deposit, "Crypto Investment", request.Name));
                cc.Deposit = 0;
                continue;
            }
        }

        var crypto = new CryptoCurrency(request.Name, request.Value, request.Investment);

        await _unitOfWork.CryptoRepository.AddCrypto(crypto);

        user.CryptoCurrencies.Add(crypto);

        await _unitOfWork.Save();

        return crypto;
    }
}
