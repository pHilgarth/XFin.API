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

        [HttpGet("{id}")]
        public IActionResult GetBankAccount(int id, bool includeTransactions = false, int year = 0, int month = 0)
        {
            var bankAccount = repo.GetBankAccount(id, includeTransactions, year, month);

            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        private readonly IBankAccountRepository repo;
    }
}
