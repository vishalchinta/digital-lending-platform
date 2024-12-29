namespace DigitalLendingPlatform.DTOs
{
    public class AddTransactionRequest
    {
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }  // Buy or Sell
    }
}