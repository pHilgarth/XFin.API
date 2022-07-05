using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IReserveRepository
    {
        Reserve CreateReserve(ReserveCreationModel reserve);
        //List<ReserveModel> GetReserves();
        //List<ReserveSimpleModel> GetReversesSimple();
        //ReserveModel GetReserve(int id);
        //ReserveSimpleModel GetReserveSimple(int id);
        //Reserve Update(int id, JsonPatchDocument<ReserveUpdateModel> reservePatch);
    }
}
