using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        public TransactionController(ITransactionRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(TransactionCreationModel transaction)
        {
            //TODO - I made targetCostCenterId in TransactionCreationModel nullable, so its optional in frontend - in the repository I have to assign the id of the
            //TODO - 'Unallocated' costCenter to TargetCostCenter, when no id is passed from the frontend
            var newTransaction = repo.Create(transaction);
            return newTransaction != null ? Ok(newTransaction) : BadRequest();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = repo.GetAll();

            return transactions.Count > 0 ? Ok(transactions) : NoContent();
        }

        [HttpGet("bankAccount/{bankAccountId}")]
        public IActionResult GetAllByAccount(int bankAccountId)
        {
            var transactions = repo.GetAllByAccount(bankAccountId);
            return transactions.Count > 0 ? Ok(transactions) : NoContent();
        }

        private readonly ITransactionRepository repo;
    }
}