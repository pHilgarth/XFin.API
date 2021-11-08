using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/internalBankAccounts")]
    public class InternalBankAccountSettingsController : Controller
    {
        public InternalBankAccountSettingsController(IInternalBankAccountSettingsRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAccountSetting(int id)
        {
            var settings = repo.GetInternalBankAccountSettings(id);

            //if settings != null there is no account with that id
            return settings != null ? Ok(settings) : NoContent();
        }

        private readonly IInternalBankAccountSettingsRepository repo;
    }
}