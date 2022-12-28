﻿using Domain;
using System.Numerics;

namespace Application.Abstract;

public interface ICryptoRepository
{
    Task Save();
    Task AddCrypto(CryptoCurrency u);
    Task UpdateCrypto(CryptoCurrency u);
    Task DeleteCryptor(CryptoCurrency u);
    Task<CryptoCurrency> GetCryptoCurrencyById(long id);
    Task<CryptoCurrency> GetCryptoCurrencyByIban(string iban);
    Task<List<CryptoCurrency>> GetAllCryptoCurrencies();
    Task<List<CryptoCurrency>> GetCryptoCurrenciesByUserId(long userId);
}