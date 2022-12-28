using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Transaction
{
    [Key]
    public long TransactionId { get; set; }
    public long Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }

    [ForeignKey("CCId")]
    public CreditCard CC { get; set; }

    public Transaction(long amount, CreditCard cc)
    {
        Amount = amount;
        CC = cc;
        DateOfTransaction = DateTime.UtcNow;
    }

    public Transaction()
    {
    }
}
