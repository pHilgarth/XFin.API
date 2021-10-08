using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/internalTransactions")]
    public class InternalTransactionController : Controller
    {
        public InternalTransactionController(IInternalTransactionRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost()]
        public IActionResult CreateTransaction(InternalTransactionCreationModel transaction)
        {
            var newTransaction = repo.CreateInternalTransaction(transaction);
            return Ok(newTransaction);
        }

        private readonly IInternalTransactionRepository repo;
    }
}
