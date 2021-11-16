using Microsoft.AspNetCore.Mvc;
using XFin.API.DAL.Interfaces;

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

        [HttpGet]
        public IActionResult GetTransactionCategories()
        {
            var transactionCategories = repo.GetAll();

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionCategoriesByBankAccount(int id, bool simple = false, int year = 0, int month = 0)
        {
            var transactionCategories = repo.GetAllByAccount(id, year, month);

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        //this endpoint gets all categories for a specified account without transactions but with blockedBudget
        [HttpGet("simple/{id}")]
        public IActionResult GetAllSimpleByAccount(int id)
        {
            var transactionCategories = repo.GetAllSimpleByAccount(id);

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        private ITransactionCategoryRepository repo;
    }
}
