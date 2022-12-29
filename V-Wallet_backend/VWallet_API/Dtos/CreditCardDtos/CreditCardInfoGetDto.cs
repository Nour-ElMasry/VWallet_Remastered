namespace VWallet_API.Dtos.CreditCardDtos;

public class CreditCardInfoGetDto
{
    public long CreditCardId { get; set; }
    public String Iban { get; set; }
    public DateTime ExpirtationDate { get; set; }
    public long Deposit { get; set; }
}
