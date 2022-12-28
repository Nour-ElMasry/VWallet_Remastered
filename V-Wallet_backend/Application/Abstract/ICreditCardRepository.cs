using Domain;

namespace Application.Abstract;

public interface ICreditCardRepository
{
    Task Save();
    Task AddCreditCard(CreditCard u);
    Task UpdateCreditCard(CreditCard u);
    Task DeleteCreditCard(CreditCard u);
    Task<CreditCard> GetCreditCardById(long id);
    Task<CreditCard> GetCreditCard(string iban, DateOnly date, long cvv);
    Task<List<CreditCard>> GetAllCreditCards();
    Task<List<CreditCard>> GetCreditCardsByUserId(long userId);
}
