using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Transaction
{
    [Key]
    public long TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public String CCIban { get; set; }

    public Transaction(decimal amount, string transactionType,string ccIban)
    {
        Amount = amount;
        CCIban = ccIban;
        TransactionType = transactionType;
        DateOfTransaction = DateTime.UtcNow;
    }

    public Transaction()
    {
    }
}
