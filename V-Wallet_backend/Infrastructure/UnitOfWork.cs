using Application.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext, ICreditCardRepository creditCardRepository,
        ICryptoRepository cryptoRepository)
    {
        _dataContext = dataContext;
        CryptoRepository = cryptoRepository;
        CreditCardRepository = creditCardRepository;
    }

    public ICreditCardRepository CreditCardRepository { get; private set; }
    public ICryptoRepository CryptoRepository { get; private set; }

    public async Task Save()
    {
        await _dataContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }
}