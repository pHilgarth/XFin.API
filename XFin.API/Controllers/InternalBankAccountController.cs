using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/internalBankAccounts")]
    public class InternalBankAccountController : Controller
    {
        public InternalBankAccountController(IInternalBankAccountRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult CreateBankAccount(InternalBankAccountCreationModel bankAccount)
        {
            var newBankAccount = repo.CreateBankAccount(bankAccount);
            //return newBankAccount != null ? Ok(newBankAccount) : Conflict();
            if (newBankAccount != null)
            {
                return Ok(newBankAccount);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAccount(int id, bool simple = true, int year = 0, int month = 0)
        {
            if (simple)
            {
                var bankAccount = repo.GetBankAccountSimple(id, year, month);
                return bankAccount != null ? Ok(bankAccount) : NoContent();
            }
            else
            {
                var bankAccount = repo.GetBankAccount(id, year, month);
                return bankAccount != null ? Ok(bankAccount) : NoContent();
            }
        }

        //this endpoint is used to check for iban duplicates when creating new accounts
        [HttpGet("iban/{iban}")]
        public IActionResult GetBankAccountByIban(string iban)
        {
            var bankAccount = repo.GetByIban(iban);

            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch)
        {
            var updatedBankAccount = repo.UpdateBankAccountPartially(id, bankAccountPatch);

            return Ok(updatedBankAccount);
        }

        private readonly IInternalBankAccountRepository repo;
    }
}