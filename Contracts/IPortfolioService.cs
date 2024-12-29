
using DigitalLendingPlatform.Models;

namespace DigitalLendingPlatform.Contracts
{
    public interface IPortfolioService
    {
        Task<Portfolio> CreatePortfolioAsync(int userId);
        Task<Investment> AddInvestmentAsync(int portfolioId, string investmentName, decimal amountInvested, decimal currentValue);
        Task<InvestmentTransaction> AddTransactionAsync(int investmentId, decimal amount, decimal price, string type);
        Task<Portfolio> GetPortfolioAsync(int userId);
        Task<Investment> GetInvestmentAsync(int investmentId);
    }

}