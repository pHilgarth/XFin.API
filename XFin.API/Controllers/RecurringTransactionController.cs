using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/recurringTransactions")]
    public class RecurringTransactionController : Controller
    {
        public RecurringTransactionController(IRecurringTransactionRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(RecurringTransactionCreationModel recurringTransaction)
        {
            var newRecurringTransaction = repo.Create(recurringTransaction);
            return newRecurringTransaction != null ? Ok(newRecurringTransaction) : BadRequest();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var recurringTransactions = repo.GetAll();

            return recurringTransactions.Count > 0 ? Ok(recurringTransactions) : NoContent();
        }

        [HttpGet("bankAccount/{bankAccountId}")]
        public IActionResult GetAllByAccount(int bankAccountId)
        {
            var recurringTransactions = repo.GetAllByAccount(bankAccountId);
            return recurringTransactions.Count > 0 ? Ok(recurringTransactions) : NoContent();
        }

        private readonly IRecurringTransactionRepository repo;
    }
}