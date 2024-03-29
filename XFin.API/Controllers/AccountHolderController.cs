﻿using Microsoft.AspNetCore.JsonPatch;
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

            return newAccountHolder != null ? Ok(newAccountHolder) : NotFound();
        }

        //param "external" would be provided via queryString!
        [HttpGet("user/{userId}")]
        public IActionResult GetAllByUser(int userId, bool external)
        {
            var accountHolders = repo.GetAllByUser(userId, external);

            return accountHolders != null
                ? accountHolders.Count > 0 ? Ok(accountHolders) : NoContent()
                : NotFound();
        }

        [HttpGet("{accountHolderId}")]
        //public IActionResult GetAccountHolder(int id, bool includeAccounts = false, bool simpleAccounts = true)
        public IActionResult GetSingle(int accountHolderId)
        {
            var accountHolder = repo.GetSingle(accountHolderId);
            return accountHolder != null ? Ok(accountHolder) : NoContent();
        }

        //This endpoint is for duplicate checks
        [HttpGet("user/{userId}/name/{name}")]
        public IActionResult GetByUserAndName(int userId, string name)
        {
            var accountHolder = repo.GetSingleByUserAndName(userId, name);

            return accountHolder != null ? Ok(accountHolder) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch)
        {
            //TODO - error handling
            var updatedAccountHolder = repo.Update(id, accountHolderPatch);

            return updatedAccountHolder != null ? Ok(updatedAccountHolder) : NotFound();
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