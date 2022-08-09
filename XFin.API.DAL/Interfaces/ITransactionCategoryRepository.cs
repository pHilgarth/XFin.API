﻿using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ICostCenterRepository
    {
        CostCenter CreateCostCenter(CostCenterCreationModel costCenter);
        List<CostCenterSimpleModel> GetAll();
        List<CostCenterModel> GetAllByAccount(int id, int year, int month);
        CostCenter Update(int id, JsonPatchDocument<CostCenterUpdateModel> costCenterPatch);
    }
}
