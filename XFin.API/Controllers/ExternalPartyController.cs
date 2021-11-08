using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/externalParties")]
    public class ExternalPartyController : Controller
    {
        public ExternalPartyController(IExternalPartyRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost()]
        public IActionResult CreateExternalParty(ExternalPartyCreationModel externalParty)
        {
            //TODO
            //do we need some error handling here?
            var newExternalParty = repo.CreateExternalParty(externalParty);
            return Ok(newExternalParty);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetExternalParty(int id, bool includeAccount = false)
        //{
        //    if (includeAccount)
        //    {
        //        var accountHolder = repo.GetExternalParty(id);
        //        return accountHolder != null ? Ok(accountHolder) : NoContent();
        //    }
        //    else
        //    {
        //        var accountHolder = repo.GetExternalPartySimple(id);
        //        return accountHolder != null ? Ok(accountHolder) : NoContent();
        //    }
        //}

        [HttpGet()]
        public IActionResult GetExternalParties()
        {
            var externalParties = repo.GetExternalParties();

            return externalParties != null && externalParties.Count > 0
                ? Ok(externalParties)
                : NoContent();
        }

        private readonly IExternalPartyRepository repo;
    }
}
