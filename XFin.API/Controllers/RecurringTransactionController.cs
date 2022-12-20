using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("sourceAccount/{accountId}")]
        public IActionResult GetAllBySourceAccount(int accountId)
        {
            var recurringTransactions = repo.GetAllBySourceAccount(accountId);
            return recurringTransactions.Count > 0 ? Ok(recurringTransactions) : NoContent();
        }

        [HttpGet("targetAccount/{accountId}")]
        public IActionResult GetAllByTargetAccount(int accountId)
        {
            var recurringTransactions = repo.GetAllByTargetAccount(accountId);
            return recurringTransactions.Count > 0 ? Ok(recurringTransactions) : NoContent();
        }

        [HttpGet("{year}/{month}/{day}")]
        public IActionResult GetAllByDueDate(int year, int month, int day)
        {
            var recurringTransactions = repo.GetAllByDueDate(year, month, day);

            return recurringTransactions.Count > 0 ? Ok(recurringTransactions) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<RecurringTransactionUpdateModel> recurringTransactionPatch)
        {
            var updatedRecurringTransaction = repo.Update(id, recurringTransactionPatch);

            return updatedRecurringTransaction != null ? Ok(updatedRecurringTransaction) : NotFound();
        }

        private readonly IRecurringTransactionRepository repo;
    }
}