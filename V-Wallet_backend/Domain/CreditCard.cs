using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class CreditCard
{
    [Key]
    public long CreditCardId { get; set; }
    public String Iban { get; set; }
    public DateTime ExpirtationDate { get; set; }
    public long Deposit { get; set; }
    public long Cvv { get; set; }

    public IList<Transaction> Transactions { get; set; }

    public CreditCard()
    {
        Iban = CreditCardBuilder.GenerateIban();
        ExpirtationDate = CreditCardBuilder.GenerateExpirationDate();
        Cvv = CreditCardBuilder.GenerateCVV();
        Deposit = CreditCardBuilder.GenerateDeposit();
        Transactions = new List<Transaction>();
    }

    public override string ToString()
    {
        return $"IBAN: {Iban}, \n Exp: {ExpirtationDate.Date}, \n CVV: {Cvv}, \n Deposit: {Deposit}";
    }
}

