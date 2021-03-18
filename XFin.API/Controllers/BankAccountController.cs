using Microsoft.AspNetCore.Mvc;
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

        private readonly IBankAccountRepository repo;
    }
}
