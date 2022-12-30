namespace VWallet_API.Dtos.CreditCardDtos;

public class CreditCardPostDto
{
    public String Iban { get; set; }
    public String ExpirtationDate { get; set; }
    public long Cvv { get; set; }
}
