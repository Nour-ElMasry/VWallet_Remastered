namespace Application.Abstract;

public interface IUnitOfWork : IDisposable
{
    Task Save();
    public ICreditCardRepository CreditCardRepository { get; }
    public ICryptoRepository CryptoRepository { get; }
}
