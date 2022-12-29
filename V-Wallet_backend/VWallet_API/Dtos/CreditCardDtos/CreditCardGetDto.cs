using VWallet_API.Dtos.TransactionDtos;

namespace VWallet_API.Dtos.CreditCardDtos;

public class CreditCardGetDto
{
    public long CreditCardId { get; set; }
    public String Iban { get; set; }
    public DateTime ExpirtationDate { get; set; }
    public long Deposit { get; set; }
    public List<TransactionGetDto> Transactions { get; set; }
}
