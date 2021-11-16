using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/internalBankAccountSettings")]
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

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<InternalBankAccountSettingsUpdateModel> settingsPatch)
        {
            //TODO - error handling
            var updatedSettings = repo.Update(id, settingsPatch);

            return updatedSettings != null ? Ok(updatedSettings) : NotFound();
        }

        private readonly IInternalBankAccountSettingsRepository repo;
    }
}