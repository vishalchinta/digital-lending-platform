using DigitalLendingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLendingPlatform.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentTransaction> InvestmentTransactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);
            modelBuilder.Entity<LoanRequest>().HasKey(l => l.Id);
            modelBuilder.Entity<LoanRepayment>().HasKey(l => l.Id);
            modelBuilder.Entity<Portfolio>().HasKey(p => p.Id);
            modelBuilder.Entity<Investment>().HasKey(i => i.Id);
            modelBuilder.Entity<InvestmentTransaction>().HasKey(i => i.Id);
            modelBuilder.Entity<Notification>().HasKey(n => n.Id);
            modelBuilder.Entity<User>()
                        .HasOne(u => u.Portfolio)
                        .WithOne(p => p.User)
                        .HasForeignKey<Portfolio>(p => p.UserId);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "vishalchinta27@gmail.com", FirstName = "Vishal", LastName = "Chinta", PasswordHash = "admin@4321", Role = "Admin" }
            );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 2, Email = "testuser@gmail.com", FirstName = "Test", LastName = "User", PasswordHash = "test@123", Role = "User" }
            );
        }
    }
}
