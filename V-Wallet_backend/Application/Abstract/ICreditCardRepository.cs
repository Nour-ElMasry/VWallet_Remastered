using Domain;

namespace Application.Abstract;

public interface ICreditCardRepository
{
    Task Save();
    Task AddCreditCard(CreditCard u);
    Task DeleteCreditCard(CreditCard u);
    Task<CreditCard> GetCreditCardById(long id);
    Task<CreditCard> GetCreditCardByIban(string iban);
    Task<CreditCard> GetCreditCard(string iban, DateTime date, long cvv);
    Task<List<CreditCard>> GetAllCreditCards();
}
