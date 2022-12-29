namespace VWallet_API.Dtos.TransactionDtos;

public class TransactionGetDto
{
    public long TransactionId { get; set; }
    public long Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public string TransactionIssuer { get; set; }
}
