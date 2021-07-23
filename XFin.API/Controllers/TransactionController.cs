using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

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

        [HttpPost()]
        public IActionResult CreateTransaction(TransactionCreationModel transaction)
        {
            var newTransaction = repo.CreateTransaction(transaction);
            return Ok(newTransaction);
        }

        private readonly ITransactionRepository repo;
    }
}
