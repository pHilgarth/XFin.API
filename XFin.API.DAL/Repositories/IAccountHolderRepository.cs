using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IAccountHolderRepository
    {
        List<AccountHolderModel> GetAccountHolders(bool includeAccounts, int year, int month);
        AccountHolderModel GetAccountHolder(int id, bool includeAccounts);
    }
}
