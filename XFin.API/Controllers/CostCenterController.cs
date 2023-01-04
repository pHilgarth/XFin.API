using System;
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

            //TODO - if no costCenter was created, what do I return, is BadRequest ok?
            return newCostCenter != null ? Ok(newCostCenter) : BadRequest();
        }


        [HttpGet("user/{userId}")]
        public IActionResult GetAllByUser(int userId)
        {
            var costCenters = repo.GetAllByUser(userId);

            return costCenters != null ? Ok(costCenters) : NoContent();
        }

        [HttpGet("user/{userId}/account/{accountId}")]
        public IActionResult GetAllByUserAndAccount(int userId, int accountId, int year = 0, int month = 0)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var costCenters = repo.GetAllByUserAndAccount(userId, accountId, year, month);

            return costCenters != null ? Ok(costCenters) : NoContent();
        }

        [HttpGet("user/{userId}/name/{name}")]
        public IActionResult GetSingleByUserAndName(int userId, string name)
        {
            var costCenter = repo.GetSingleByUserAndName(userId, name);

            return costCenter != null ? Ok(costCenter) : NotFound();
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
