using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("account/{accountId}")]
        public IActionResult GetAllByBankAccount(int accountId)
        {
            var loans = repo.GetAllByBankAccount(accountId);

            return loans != null
                ? loans.Count > 0 ? Ok(loans) : NoContent()
                : NotFound();
        }

        [HttpGet("{firstAccountId}/{secondAccountId}")]
        public IActionResult GetAllByCreditorAndDebitor(int firstAccountId, int secondAccountId)
        {
            var loans = repo.GetAllByCreditorAndDebitor(firstAccountId, secondAccountId);

            return loans != null
                ? loans.Count > 0 ? Ok(loans) : NoContent()
                : NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleById(int id)
        {
            var loan = repo.GetSingleById(id);

            return loan != null ? Ok(loan) : NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<LoanUpdateModel> loanPatch)
        {
            //TODO - error handling
            var updatedLoan = repo.Update(id, loanPatch);

            return updatedLoan != null ? Ok(updatedLoan) : NotFound();
        }

        private readonly ILoanRepository repo;
    }
}