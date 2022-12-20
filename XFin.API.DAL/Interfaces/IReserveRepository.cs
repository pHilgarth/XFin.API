using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IReserveRepository
    {
        Reserve Create(ReserveCreationModel reserve);
        List<ReserveModel> GetAll();
        List<ReserveModel> GetAllByAccount(int accountId);
        List<ReserveModel> GetAllByAccountAndCostCenter(int accountId, int costCenterId);
        List<ReserveModel> GetAllByCostCenter(int costCenterId);
        ReserveModel Update(int reserveId, JsonPatchDocument<ReserveUpdateModel> jsonPatchDocument);
    }
}
