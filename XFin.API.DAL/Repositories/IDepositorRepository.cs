using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IDepositorRepository
    {
        List<DepositorModel> GetDepositors(bool includeAccounts);
        DepositorModel GetDepositor(int id, bool includeAccounts);
    }
}
