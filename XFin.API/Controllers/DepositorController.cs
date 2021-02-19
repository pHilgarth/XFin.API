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
            var depositors = repo.GetDepositors(includeAccounts);

            return depositors != null ? Ok(depositors) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetDepositor(int id, bool includeAccounts = false)
        {
            var depositor = repo.GetDepositor(id, includeAccounts);

            return depositor != null ? Ok(depositor) : NotFound();
        }

        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        private readonly IDepositorRepository repo;
    }
}
