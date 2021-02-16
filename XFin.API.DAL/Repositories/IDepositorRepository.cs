using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.Repositories
{
    public interface IDepositorRepository
    {
        List<Depositor> GetDepositors(bool includeAccounts);
    }
}
