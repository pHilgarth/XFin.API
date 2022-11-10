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

            //TODO - if no accountHolder was created, what do I return, is BadRequest ok?
            return newCostCenterAsset != null ? Ok(newCostCenterAsset) : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByCostCenter(int id)
        {
            var costCenterAssets = repo.GetAllByCostCenter(id);

            return costCenterAssets != null ? Ok(costCenterAssets) : NoContent();
        }

        private ICostCenterAssetRepository repo;
    }
}
