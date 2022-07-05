using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;
//TODO - return NoContent when there are no records - on every endpoint, even on TransactionCategory, which always should
//      have records. SHOULD HAVE - you'll never know
//TODO - maybe change the action names just to "Get", "GetByName", "Create", the controller name tells, what record(s) to get
namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/reserves")]
    public class ReserveController : Controller
    {
        public ReserveController(IReserveRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost()]
        public IActionResult CreateReserve(ReserveCreationModel reserve)
        {
            var newReserve = repo.CreateReserve(reserve);

            //TODO - if no accountHolder was created, what do I return, is BadRequest ok?
            return newReserve != null ? Ok(newReserve) : BadRequest();
        }

        //[HttpGet("{id}")]
        ////public IActionResult GetAccountHolder(int id, bool includeAccounts = false, bool simpleAccounts = true)
        //public IActionResult GetAccountHolder(int id, bool includeAccounts = false)
        //{
        //    if (includeAccounts)
        //    {
        //        //var accountHolder = repo.GetAccountHolder(id, simpleAccounts);
        //        var accountHolder = repo.GetAccountHolder(id);
        //        return accountHolder != null ? Ok(accountHolder) : NoContent();
        //    }
        //    else
        //    {
        //        var accountHolder = repo.GetAccountHolderSimple(id);
        //        return accountHolder != null ? Ok(accountHolder) : NoContent();
        //    }
        //}

        ////This endpoint is for duplicate checks
        //[HttpGet("name/{name}")]
        //public IActionResult GetByName(string name)
        //{
        //    var accountHolder = repo.GetByName(name);

        //    return accountHolder != null ? Ok(accountHolder) : NoContent();
        //}

        //[HttpGet]
        //public IActionResult GetAccountHolders(bool includeAccounts = false)
        //{
        //    if (includeAccounts)
        //    {
        //        var accountHolders = repo.GetAccountHolders();
        //        return accountHolders.Count > 0 ? Ok(accountHolders) : NoContent();
        //    }
        //    else
        //    {
        //        var accountHolders = repo.GetAccountHoldersSimple();
        //        return accountHolders.Count > 0 ? Ok(accountHolders) : NoContent();
        //    }

        //}

        //[HttpPatch("{id}")]
        //public IActionResult Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch)
        //{
        //    //TODO - error handling
        //    //TODO - check if this variable name is correct - its on accountHolderController and variable is called updatedBankAccout??
        //    var updatedBankAccount = repo.Update(id, accountHolderPatch);

        //    return updatedBankAccount != null ? Ok(updatedBankAccount) : NotFound();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteAccountHolder(int id)
        //{
        //    return BadRequest();
        //}

        private readonly IReserveRepository repo;
    }
}