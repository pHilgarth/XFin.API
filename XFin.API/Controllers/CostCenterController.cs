using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/costCenters")]
    public class CostCenterController : Controller
    {
        public CostCenterController(ICostCenterRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(CostCenterCreationModel costCenter)
        {
            var newCostCenter = repo.Create(costCenter);

            //TODO - if no accountHolder was created, what do I return, is BadRequest ok?
            return newCostCenter != null ? Ok(newCostCenter) : BadRequest();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var costCenters = repo.GetAll();

            return costCenters != null ? Ok(costCenters) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByAccount(int id, int year = 0, int month = 0)
        {
            var costCenters = repo.GetAllByAccount(id, year, month);

            return costCenters != null ? Ok(costCenters) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<CostCenterUpdateModel> costCenterPatch)
        {
            //TODO - error handling
            var updatedCostCenter = repo.Update(id, costCenterPatch);

            return updatedCostCenter != null ? Ok(updatedCostCenter) : NotFound();
        }

        private ICostCenterRepository repo;
    }
}
