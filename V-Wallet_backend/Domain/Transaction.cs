using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Transaction
{
    [Key]
    public long TransactionId { get; set; }
    public long Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    
    [ForeignKey("ReceivingCCId")]
    public CreditCard ReceivingCC { get; set; }

    [ForeignKey("SendingCCId")]
    public CreditCard SendingCC { get; set; }

    public Transaction(long amount, CreditCard receivingCC, CreditCard sendingCC)
    {
        Amount = amount;
        ReceivingCC = receivingCC;
        SendingCC = sendingCC;
        DateOfTransaction = DateTime.UtcNow;
    }

    public Transaction()
    {
    }
}
