using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;
//TODO - IMPORTANT -> check for modelstate, check for duplicated names/titles/references.... when posting or updating objects!!
            //in general: more error handling...


//TODO - return NoContent when there are no records - on every endpoint, even on CostCenter, which always should
//      have records. SHOULD HAVE - you'll never know
//TODO - maybe change the action names just to "Get", "GetByName", "Create", the controller name tells, what record(s) to get
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
        public IActionResult Create(AccountHolderCreationModel accountHolder)
        {
            var newAccountHolder = repo.Create(accountHolder);

            //TODO - if no accountHolder was created, what do I return, is NoContent the right thing?
            return newAccountHolder != null ? Ok(newAccountHolder) : NoContent();
        }

        [HttpGet("{userId}")]
        public IActionResult GetAllByUser(int userId)
        {
            var accountHolders = repo.GetAllByUser(userId);
            return accountHolders.Count > 0 ? Ok(accountHolders) : NoContent();
        }

        [HttpGet("{userId}/{accountHolderId}")]
        //public IActionResult GetAccountHolder(int id, bool includeAccounts = false, bool simpleAccounts = true)
        public IActionResult GetSingle(int userId, int accountHolderId)
        {
            var accountHolder = repo.GetSingle(userId, accountHolderId);
            return accountHolder != null ? Ok(accountHolder) : NoContent();
        }

        //This endpoint is for duplicate checks
        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var accountHolder = repo.GetByName(name);

            return accountHolder != null ? Ok(accountHolder) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch)
        {
            //TODO - error handling
            //TODO - check if this variable name is correct - its on accountHolderController and variable is called updatedBankAccout??
            var updatedBankAccount = repo.Update(id, accountHolderPatch);

            return updatedBankAccount != null ? Ok(updatedBankAccount) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //TODO - implement it ;)
            return BadRequest();
        }
        private readonly IAccountHolderRepository repo;
    }
}