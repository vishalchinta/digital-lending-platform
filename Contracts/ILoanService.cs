
using DigitalLendingPlatform.Models;

namespace DigitalLendingPlatform.Contracts
{
    public interface ILoanService
    {
        Task<Loan> ApplyForLoanAsync(string userId, decimal amount, decimal interestRate, int termInMonths);
        Task<Loan> ApproveLoanAsync(int loanRequestId);
        Task<LoanRepayment> MakeRepaymentAsync(int loanId, decimal repaymentAmount);
        Task<Loan> GetLoanByIdAsync(int loanId);
    }

}