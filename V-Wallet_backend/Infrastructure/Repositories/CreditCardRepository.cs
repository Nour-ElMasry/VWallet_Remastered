using Application.Abstract;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    private readonly DataContext _context;

    public CreditCardRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task AddCreditCard(CreditCard u)
    {
        await _context.CreditCards.AddAsync(u);
    }

    public async Task DeleteCreditCard(CreditCard u)
    {
        await Task.Run(() => _context.CreditCards.Remove(u));
    }

    public async Task<List<CreditCard>> GetAllCreditCards()
    {
        return await _context.CreditCards.ToListAsync();
    }

    public async Task<CreditCard> GetCreditCard(string iban, DateTime date, long cvv)
    {
        return await _context.CreditCards
            .SingleOrDefaultAsync(cc => cc.Iban == iban
            && cc.ExpirtationDate.Month == date.Month
            && cc.ExpirtationDate.Year == date.Year
            && cc.Cvv == cvv); ;
    }

    public async Task<CreditCard> GetCreditCardByIban(string iban)
    {
        return await _context.CreditCards
            .Include(cc => cc.Transactions)
            .SingleOrDefaultAsync(cc => cc.Iban == iban);
    }

    public async Task<CreditCard> GetCreditCardById(long id)
    {
        return await _context.CreditCards
            .SingleOrDefaultAsync(cc => cc.CreditCardId == id);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
