using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
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

            return accountHolder != null ? Ok(accountHolder) : NoContent();
        }

        [HttpPost()]
        public IActionResult CreateAccountHolder(AccountHolderCreationModel accountHolder)
        {
            //TODO
            //do we need some error handling here?
            var newAccountHolder = repo.CreateAccountHolder(accountHolder);
            return Ok(newAccountHolder);
        }

        private readonly IAccountHolderRepository repo;
    }
}
