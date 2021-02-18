using Microsoft.AspNetCore.Mvc;
using XFin.API.DAL.Repositories;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/depositors")]
    public class DepositorController : Controller
    {
        /*************************************************************************************************************
         * 
         * Constructors
         * 
        *************************************************************************************************************/
        public DepositorController(IDepositorRepository repo)
        {
            this.repo = repo;
        }
        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        [HttpGet()]
        public IActionResult GetDepositors(bool includeAccounts = false)
        {
            return Ok(repo.GetDepositors(includeAccounts));
        }
        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        private readonly IDepositorRepository repo;
    }
}
