using Domain;
using System.Numerics;

namespace Application.Abstract;

public interface ICryptoRepository
{
    Task Save();
    Task AddCrypto(CryptoCurrency u);
    Task DeleteCrypto(CryptoCurrency u);
    Task<CryptoCurrency> GetCryptoCurrencyById(long id);
    Task<List<CryptoCurrency>> GetAllCryptoCurrencies();
}
