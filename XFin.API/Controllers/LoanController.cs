using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : Controller
    {
        public LoanController(ILoanRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(LoanCreationModel loan)
        {
            var newLoan = repo.Create(loan);
            return newLoan != null ? Ok(newLoan) : BadRequest();
        }

        [HttpGet]
        public IActionResult GetLoans()
        {
            var loans = repo.GetAll();

            return loans.Count > 0 ? Ok(loans) : NoContent();
        }

        [HttpGet("bankAccount/{bankAccountId}")]
        public IActionResult GetAllByAccount(int bankAccountId)
        {
            var loans = repo.GetAllByAccount(bankAccountId);
            return loans.Count > 0 ? Ok(loans) : NoContent();
        }

        private readonly ILoanRepository repo;
    }
}