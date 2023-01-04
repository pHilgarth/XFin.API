using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
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

        [HttpGet("user/{userId}")]
        public IActionResult GetAllByUser(int userId)
        {
            var bankAccounts = repo.GetAllByUser(userId);
            return bankAccounts.Count > 0 ? Ok(bankAccounts) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id, int year = 0, int month = 0)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var bankAccount = repo.GetSingle(id, year, month);
            return bankAccount != null ? Ok(bankAccount) : NoContent();
        }

        //this endpoint is used to check for iban duplicates when creating new accounts
        [HttpGet("user/{userId}/iban/{iban}")]
        public IActionResult GetSingleByUserAndIban(int userId, string iban)
        {
            var bankAccount = repo.GetSingleByUserAndIban(userId, iban);

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