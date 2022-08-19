using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/bankAccounts")]
    public class BankAccountController : Controller
    {
        public BankAccountController(IBankAccountRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(BankAccountCreationModel bankAccount)
        {
            var newBankAccount = repo.Create(bankAccount);
            return newBankAccount != null ? Ok(newBankAccount) : NotFound();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bankAccounts = repo.GetAll();
            return bankAccounts.Count > 0 ? Ok(bankAccounts) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id, int year = 0, int month = 0)
        {
            var bankAccount = repo.GetSingle(id, year, month);
            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        //this endpoint is used to check for iban duplicates when creating new accounts
        [HttpGet("iban/{iban}")]
        public IActionResult GetByIban(string iban)
        {
            var bankAccount = repo.GetByIban(iban);

            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<BankAccountUpdateModel> bankAccountPatch)
        {
            //TODO - error handling
            var updatedBankAccount = repo.Update(id, bankAccountPatch);

            return updatedBankAccount != null ? Ok(updatedBankAccount) : NotFound();
        }

        private readonly IBankAccountRepository repo;
    }
}