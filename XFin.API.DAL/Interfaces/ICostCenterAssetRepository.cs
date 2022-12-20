using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ICostCenterAssetRepository
    {
        CostCenterAsset Create(CostCenterAssetCreationModel costCenterAsset);
        List<CostCenterAssetModel> GetAllByAccountAndCostCenter(int accountId, int costCenterId);
        CostCenterAssetModel Update(int id, JsonPatchDocument<CostCenterAssetUpdateModel> costCenterAssetPatch);
    }
}
