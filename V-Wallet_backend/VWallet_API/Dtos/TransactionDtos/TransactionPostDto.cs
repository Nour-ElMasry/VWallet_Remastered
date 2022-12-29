namespace VWallet_API.Dtos.TransactionDtos
{
    public class TransactionPostDto
    {
        public string SendingCCIban { get; set; }
        public string ReceivingCCIban { get; set; }
        public long Amount { get; set; }
    }
}
