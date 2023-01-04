using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ICostCenterRepository
    {
        CostCenter Create(CostCenterCreationModel costCenter);
        List<CostCenterSimpleModel> GetAllByUser(int userId);
        List<CostCenterModel> GetAllByUserAndAccount(int userId, int accountId, int year, int month);
        CostCenterSimpleModel GetSingleByUserAndName(int userId, string name);
        CostCenter Update(int id, JsonPatchDocument<CostCenterUpdateModel> costCenterPatch);
    }
}
