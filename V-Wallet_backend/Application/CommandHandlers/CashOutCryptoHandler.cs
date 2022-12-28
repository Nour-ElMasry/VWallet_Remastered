using Application.Abstract;
using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers;

public class CashOutCryptoHandler : IRequestHandler<CashOutCrypto, CryptoCurrency>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CashOutCryptoHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<CryptoCurrency> Handle(CashOutCrypto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.CryptoCurrencies)
            .Include(u => u.CreditCards)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return null;

        var crypto = user.CryptoCurrencies.FirstOrDefault(c => c.CryptoId == request.CryptoId);

        if (crypto == null)
            return null;

        var moneyPerCC = (request.CurrentValue * (crypto.Investment / crypto.Value)) / user.CreditCards.Count;

        foreach (var cc in user.CreditCards)
        {
            cc.Deposit += moneyPerCC;
        }

        await _unitOfWork.CryptoRepository.DeleteCrypto(crypto);
        await _unitOfWork.Save();

        return crypto;
    }
}
