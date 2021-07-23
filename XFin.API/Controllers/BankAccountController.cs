using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/bankAccounts")]
    public class BankAccountController : Controller
    {
        public BankAccountController(IBankAccountRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{accountNumber}")]
        public IActionResult GetBankAccount(string accountNumber, bool includeTransactions = false, int year = 0, int month = 0)
        {
            var bankAccount = repo.GetBankAccount(accountNumber, includeTransactions, year, month);

            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        [HttpPost]
        public IActionResult CreateBankAccount(BankAccountCreationModel bankAccount)
        {
            var newBankAccount = repo.CreateBankAccount(bankAccount);
            return Ok(newBankAccount);
        }

        private readonly IBankAccountRepository repo;
    }
}
