using System;

namespace DigitalLendingPlatform.Models
{
    public class LoanRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int TermInMonths { get; set; }  // Loan term in months
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }  // Pending, Approved, Rejected
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int TermInMonths { get; set; }
        public DateTime ApprovedAt { get; set; }
        public DateTime? DisbursedAt { get; set; } // When the loan is disbursed to the user
        public string Status { get; set; }  // Active, Closed
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class LoanRepayment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal RepaymentAmount { get; set; }
        public DateTime RepaymentDate { get; set; }
        public bool IsPaid { get; set; }  // If the repayment is successful
        public virtual Loan Loan { get; set; }
    }
}