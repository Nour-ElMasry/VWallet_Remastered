using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Transaction
{
    [Key]
    public long TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public String CCIban { get; set; }

    public Transaction(long amount, string ccIban)
    {
        Amount = amount;
        CCIban = ccIban;
        DateOfTransaction = DateTime.UtcNow;
    }

    public Transaction()
    {
    }
}
