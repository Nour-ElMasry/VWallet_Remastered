using Application.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CryptoRepository : ICryptoRepository
{
    private readonly DataContext _context;

    public CryptoRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task AddCrypto(CryptoCurrency u)
    {
        await _context.CryptoCurrencies.AddAsync(u);
    }

    public async Task DeleteCrypto(CryptoCurrency u)
    {
        await Task.Run(() => _context.CryptoCurrencies.Remove(u));
    }

    public async Task<List<CryptoCurrency>> GetAllCryptoCurrencies()
    {
        return await _context.CryptoCurrencies.ToListAsync();
    }

    public async Task<CryptoCurrency> GetCryptoCurrencyById(long id)
    {
        return await _context.CryptoCurrencies
           .SingleOrDefaultAsync(cc => cc.CryptoId == id);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
