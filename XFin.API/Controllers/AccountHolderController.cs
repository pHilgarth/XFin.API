using Microsoft.AspNetCore.Mvc;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/accountHolders")]
    public class AccountHolderController : Controller
    {
        public AccountHolderController(IAccountHolderRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet()]
        public IActionResult GetAccountHolders(bool includeAccounts = false)
        {
            var accountHolders = repo.GetAccountHolders(includeAccounts);

            return accountHolders != null ? Ok(accountHolders) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountHolder(int id, bool includeAccounts = false)
        {
            var accountHolder = repo.GetAccountHolder(id, includeAccounts);

            return accountHolder != null ? Ok(accountHolder) : NotFound();
        }

        private readonly IAccountHolderRepository repo;
    }
}
