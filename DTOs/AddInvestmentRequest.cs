namespace DigitalLendingPlatform.DTOs
{
    public class AddInvestmentRequest
    {
        public int PortfolioId { get; set; }
        public string InvestmentName { get; set; }
        public decimal AmountInvested { get; set; }
        public decimal CurrentValue { get; set; }
    }
}