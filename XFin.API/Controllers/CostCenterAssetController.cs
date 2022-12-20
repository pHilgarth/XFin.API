using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/costCenterAssets")]
    public class CostCenterAssetController : Controller
    {
        public CostCenterAssetController(ICostCenterAssetRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(CostCenterAssetCreationModel costCenterAsset)
        {
            var newCostCenterAsset = repo.Create(costCenterAsset);

            //TODO - if no costCenterAsset was created, what do I return, is BadRequest ok?
            return newCostCenterAsset != null ? Ok(newCostCenterAsset) : BadRequest();
        }

        [HttpGet("{accountId}/{costCenterId}")]
        public IActionResult GetAllByAccountAndCostCenter(int accountId, int costCenterId)
        {
            var costCenterAssets = repo.GetAllByAccountAndCostCenter(accountId, costCenterId);

            return costCenterAssets != null ? Ok(costCenterAssets) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<CostCenterAssetUpdateModel> costCenterAssetPatch)
        {
            //TODO - error handling
            var updatedCostCenterAsset = repo.Update(id, costCenterAssetPatch);

            return updatedCostCenterAsset != null ? Ok(updatedCostCenterAsset) : NotFound();
        }

        private ICostCenterAssetRepository repo;
    }
}
