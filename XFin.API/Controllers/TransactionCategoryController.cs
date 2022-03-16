using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
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

        [HttpPost]
        public IActionResult Create(TransactionCategoryCreationModel transactionCategory)
        {
            var newTransactionCategory = repo.CreateTransactionCategory(transactionCategory);

            //TODO - if no accountHolder was created, what do I return, is BadRequest ok?
            return newTransactionCategory != null ? Ok(newTransactionCategory) : BadRequest();
        }


        [HttpGet]
        public IActionResult GetTransactionCategories()
        {
            var transactionCategories = repo.GetAll();

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByAccount(int id, int year = 0, int month = 0)
        {
            var transactionCategories = repo.GetAllByAccount(id, year, month);

            return transactionCategories != null ? Ok(transactionCategories) : NoContent();
        }

        private ITransactionCategoryRepository repo;
    }
}
