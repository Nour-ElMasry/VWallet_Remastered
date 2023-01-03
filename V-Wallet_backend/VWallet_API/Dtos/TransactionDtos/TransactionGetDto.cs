namespace VWallet_API.Dtos.TransactionDtos;

public class TransactionGetDto
{
    public long TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public string TransactionType { get; set; }
    public string TransactionIssuer { get; set; }
}
