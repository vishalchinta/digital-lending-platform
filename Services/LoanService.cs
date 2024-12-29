using System;
using System.Collections.Generic;
using DigitalLendingPlatform.Contracts;
using DigitalLendingPlatform.Infrastructure.Context;
using DigitalLendingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLendingPlatform.Repositories
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> ApplyForLoanAsync(string userId, decimal amount, decimal interestRate, int termInMonths)
        {
            var loanRequest = new LoanRequest
            {
                Amount = amount,
                InterestRate = interestRate,
                TermInMonths = termInMonths,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                UserId = userId
            };

            _context.LoanRequests.Add(loanRequest);
            await _context.SaveChangesAsync();

            return new Loan
            {
                Amount = amount,
                InterestRate = interestRate,
                TermInMonths = termInMonths,
                Status = "Pending",
                UserId = userId
            };
        }

        public async Task<Loan> ApproveLoanAsync(int loanRequestId)
        {
            var loanRequest = await _context.LoanRequests
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Id == loanRequestId);

            if (loanRequest == null || loanRequest.Status != "Pending")
            {
                throw new InvalidOperationException("Loan request is either not found or already processed.");
            }

            loanRequest.Status = "Approved";

            var loan = new Loan
            {
                Amount = loanRequest.Amount,
                InterestRate = loanRequest.InterestRate,
                TermInMonths = loanRequest.TermInMonths,
                ApprovedAt = DateTime.Now,
                Status = "Active",
                UserId = loanRequest.UserId
            };

            _context.Loans.Add(loan);
            _context.LoanRequests.Update(loanRequest);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<LoanRepayment> MakeRepaymentAsync(int loanId, decimal repaymentAmount)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == loanId);
            if (loan == null || loan.Status != "Active")
            {
                throw new InvalidOperationException("Loan not found or already closed.");
            }

            var repayment = new LoanRepayment
            {
                LoanId = loanId,
                RepaymentAmount = repaymentAmount,
                RepaymentDate = DateTime.Now,
                IsPaid = true
            };

            loan.Status = "Closed";  // Mark loan as closed after repayment

            _context.LoanRepayments.Add(repayment);
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();

            return repayment;
        }

        public async Task<Loan> GetLoanByIdAsync(int loanId)
        {
            return await _context.Loans
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Id == loanId);
        }
    }

}