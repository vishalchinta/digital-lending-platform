using DigitalLendingPlatform.Contracts;
using DigitalLendingPlatform.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLendingPlatform.Controllers
{
    [Authorize]
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost("createPortfolio")]
        public async Task<IActionResult> CreatePortfolio()
        {
            var userId = User.Identity.Name;  // Get the current user’s ID
            var portfolio = await _portfolioService.CreatePortfolioAsync(int.Parse(userId));
            return Ok(portfolio);
        }

        [HttpPost("addInvestment")]
        public async Task<IActionResult> AddInvestment([FromBody] AddInvestmentRequest model)
        {
            var portfolio = await _portfolioService.AddInvestmentAsync(model.PortfolioId, model.InvestmentName, model.AmountInvested, model.CurrentValue);
            return Ok(portfolio);
        }

        [HttpPost("addTransaction/{investmentId}")]
        public async Task<IActionResult> AddTransaction(int investmentId, [FromBody] AddTransactionRequest model)
        {
            var transaction = await _portfolioService.AddTransactionAsync(investmentId, model.Amount, model.Price, model.Type);
            return Ok(transaction);
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var userId = User.Identity.Name;
            var portfolio = await _portfolioService.GetPortfolioAsync(int.Parse(userId));
            return Ok(portfolio);
        }

        [HttpGet("{investmentId}")]
        public async Task<IActionResult> GetInvestment(int investmentId)
        {
            var investment = await _portfolioService.GetInvestmentAsync(investmentId);
            if (investment == null)
            {
                return NotFound();
            }

            return Ok(investment);
        }
    }
}
