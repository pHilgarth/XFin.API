using Microsoft.AspNetCore.Mvc;
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

        [HttpPost()]
        public IActionResult CreateAccountHolder(AccountHolderCreationModel accountHolder)
        {
            //TODO - improve error handling - I need to know when it fails due to duplicate records
            var newAccountHolder = repo.CreateAccountHolder(accountHolder);

            return newAccountHolder != null ? Ok(newAccountHolder) : Conflict();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountHolder(int id, bool includeAccounts = false, bool simpleAccounts = true)
        {
            if (includeAccounts)
            {
                var accountHolder = repo.GetAccountHolder(id, simpleAccounts);
                return accountHolder != null ? Ok(accountHolder) : NoContent();
            }
            else
            {
                var accountHolder = repo.GetAccountHolderSimple(id);
                return accountHolder != null ? Ok(accountHolder) : NoContent();
            }
        }

        [HttpGet()]
        public IActionResult GetAccountHolders(bool includeAccounts = false)
        {
            if (includeAccounts)
            {
                var accountHolders = repo.GetAccountHolders();
                return accountHolders.Count > 0 ? Ok(accountHolders) : NoContent();
            }
            else
            {
                var accountHolders = repo.GetAccountHoldersSimple();
                return accountHolders.Count > 0 ? Ok(accountHolders) : NoContent();
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccountHolder(int id, AccountHolderUpdateModel accountHolder)
        {
            var updatedAccountHolder = repo.UpdateAccountHolder(id, accountHolder);

            return Ok(updatedAccountHolder);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccountHolder(int id)
        {
            return BadRequest();
        }
        private readonly IAccountHolderRepository repo;
    }
}
