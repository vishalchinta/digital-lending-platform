using System;
using System.Collections.Generic;

namespace DigitalLendingPlatform.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalValue { get; set; } // Total value of the portfolio
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public ICollection<Investment> Investments { get; set; }
    }

    public class Investment
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string InvestmentName { get; set; } // For example, Stock or Mutual Fund
        public decimal AmountInvested { get; set; }
        public decimal CurrentValue { get; set; } // Current market value of the investment
        public DateTime InvestedAt { get; set; }
        public DateTime LastUpdated { get; set; } // To track the value change

        public Portfolio Portfolio { get; set; }
        public ICollection<InvestmentTransaction> InvestmentTransactions { get; set; }
    }

    public class InvestmentTransaction
    {
        public int Id { get; set; }
        public int InvestmentId { get; set; }
        public decimal Amount { get; set; }  // Amount of the asset bought/sold
        public decimal Price { get; set; }   // Price of the asset at the time of the transaction
        public string Type { get; set; }     // Buy or Sell
        public DateTime TransactionDate { get; set; }
        public Investment Investment { get; set; }
    }
}