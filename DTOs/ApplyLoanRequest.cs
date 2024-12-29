namespace DigitalLendingPlatform.DTOs
{
    public class ApplyLoanRequest
    {
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }  // Interest rate in percentage
        public int TermInMonths { get; set; }  // Loan term in months
    }

}