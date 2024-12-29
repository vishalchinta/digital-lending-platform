using System;
using System.Collections.Generic;
using DigitalLendingPlatform.Contracts;
using DigitalLendingPlatform.Infrastructure.Context;
using DigitalLendingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLendingPlatform.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ApplicationDbContext _context;

        public PortfolioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreatePortfolioAsync(int userId)
        {
            var portfolio = new Portfolio
            {
                UserId = userId,
                TotalValue = 0,
                CreatedAt = DateTime.Now
            };

            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Investment> AddInvestmentAsync(int portfolioId, string investmentName, decimal amountInvested, decimal currentValue)
        {
            var investment = new Investment
            {
                PortfolioId = portfolioId,
                InvestmentName = investmentName,
                AmountInvested = amountInvested,
                CurrentValue = currentValue,
                InvestedAt = DateTime.Now,
                LastUpdated = DateTime.Now
            };

            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task<InvestmentTransaction> AddTransactionAsync(int investmentId, decimal amount, decimal price, string type)
        {
            var investment = await _context.Investments.FirstOrDefaultAsync(i => i.Id == investmentId);

            if (investment == null)
                throw new InvalidOperationException("Investment not found.");

            var transaction = new InvestmentTransaction
            {
                InvestmentId = investmentId,
                Amount = amount,
                Price = price,
                Type = type,
                TransactionDate = DateTime.Now
            };

            if (type.ToLower() == "buy")
            {
                investment.AmountInvested += amount;
            }
            else if (type.ToLower() == "sell")
            {
                investment.AmountInvested -= amount;
            }

            investment.LastUpdated = DateTime.Now;
            investment.CurrentValue = investment.AmountInvested * price;  // Update the current value

            _context.InvestmentTransactions.Add(transaction);
            _context.Investments.Update(investment);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Portfolio> GetPortfolioAsync(int userId)
        {
            return await _context.Portfolios
                .Include(p => p.Investments)
                .ThenInclude(i => i.InvestmentTransactions)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Investment> GetInvestmentAsync(int investmentId)
        {
            return await _context.Investments
                .Include(i => i.InvestmentTransactions)
                .FirstOrDefaultAsync(i => i.Id == investmentId);
        }

    }
}