using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/externalTransactions")]
    public class ExternalTransactionController : Controller
    {
        public ExternalTransactionController(IExternalTransactionRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost()]
        public IActionResult CreateExternalTransaction(ExternalTransactionCreationModel transaction)
        {
            var newTransaction = repo.CreateExternalTransaction(transaction);
            return Ok(newTransaction);
        }

        private readonly IExternalTransactionRepository repo;
    }
}
