using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/externalBankAccounts")]
    public class ExternalBankAccountController : Controller
    {
        public ExternalBankAccountController(IExternalBankAccountRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult CreateExternalBankAccount(ExternalBankAccountCreationModel bankAccount)
        {
            var newBankAccount = repo.CreateExternalBankAccount(bankAccount);
            return newBankAccount != null ? Ok(newBankAccount) : BadRequest();
        }

        private readonly IExternalBankAccountRepository repo;
    }
}
