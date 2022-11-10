using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;
//TODO - return NoContent when there are no records - on every endpoint, even on CostCenter, which always should
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
        public IActionResult Create(ReserveCreationModel reserve)
        {
            var newReserve = repo.Create(reserve);

            //TODO - if no accountHolder was created, what do I return, is BadRequest ok?
            return newReserve != null ? Ok(newReserve) : BadRequest();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reserves = repo.GetAll();

            return reserves.Count > 0 ? Ok(reserves) : NoContent();
        }

        //TODO - I have to check if I return the right stuff here - on "GetAllByAccountHolder" I return "reserves != null ? ..."
        // but here I check for Count -> I should be more consistent here and make sure everything works then

        //Update: on this method, everything is fine - the repo.GetAllByAccount returns an empty list, if no reserves are found for the given accountId
        [HttpGet("account/{accountId}")]
        public IActionResult GetAllByAccount(int accountId)
        {
            var reserves = repo.GetAllByAccount(accountId);

            return reserves.Count > 0 ? Ok(reserves) : NoContent();
        }

        [HttpGet("{accountId}/{costCenterId}")]
        public IActionResult GetAllByAccountAndCostCenter(int accountId, int costCenterId)
        {
            var reserves = repo.GetAllByAccountAndCostCenter(accountId, costCenterId);

            return reserves.Count > 0 ? Ok(reserves) : NoContent();
        }

        [HttpGet("costCenter/{costCenterId}")]
        public IActionResult GetAllByCostCenter(int costCenterId)
        {
            var reserves = repo.GetAllByCostCenter(costCenterId);

            return reserves.Count > 0 ? Ok(reserves) : NoContent();
        }

        private readonly IReserveRepository repo;
    }
}