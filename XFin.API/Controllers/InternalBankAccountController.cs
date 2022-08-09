﻿using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var bankAccounts = repo.GetAll();
            return bankAccounts.Count > 0 ? Ok(bankAccounts) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAccount(int id, int year = 0, int month = 0)
        {
            var bankAccount = repo.GetBankAccount(id, year, month);
            return bankAccount != null ? Ok(bankAccount) : NoContent();
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
            //TODO - error handling
            var updatedBankAccount = repo.UpdateBankAccountPartially(id, bankAccountPatch);

            return updatedBankAccount != null ? Ok(updatedBankAccount) : NotFound();
        }

        private readonly IInternalBankAccountRepository repo;
    }
}