using Microsoft.AspNetCore.Mvc;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/transactionCategories")]
    public class TransactionCategoryController : Controller
    {
        public TransactionCategoryController(ITransactionCategoryRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionCategoriesByBankAccount(int id, int year = 0, int month = 0)
        {
            var transactionCategories = repo.GetTransactionCategoriesByBankAccount(id, year, month);

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        private ITransactionCategoryRepository repo;
    }
}
