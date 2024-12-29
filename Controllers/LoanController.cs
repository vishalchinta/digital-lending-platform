using DigitalLendingPlatform.Contracts;
using DigitalLendingPlatform.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalLendingPlatform.Controllers
{
    [Authorize]
    [Route("api/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLoan([FromBody] ApplyLoanRequest model)
        {
            try
            {
                var loan = await _loanService.ApplyForLoanAsync(User.Identity.Name, model.Amount, model.InterestRate, model.TermInMonths);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("approve/{loanRequestId}")]
        public async Task<IActionResult> ApproveLoan(int loanRequestId)
        {
            try
            {
                var loan = await _loanService.ApproveLoanAsync(loanRequestId);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("repay/{loanId}")]
        public async Task<IActionResult> MakeRepayment(int loanId, [FromBody] MakeRepaymentRequest model)
        {
            try
            {
                var repayment = await _loanService.MakeRepaymentAsync(loanId, model.RepaymentAmount);
                return Ok(repayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoan(int loanId)
        {
            var loan = await _loanService.GetLoanByIdAsync(loanId);
            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }
    }
}