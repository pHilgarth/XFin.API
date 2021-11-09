using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IAccountHolderRepository
    {
        AccountHolder CreateAccountHolder(AccountHolderCreationModel accountHolder);
        List<AccountHolderModel> GetAccountHolders();
        List<AccountHolderSimpleModel> GetAccountHoldersSimple();
        AccountHolderModel GetAccountHolder(int id, bool includeTransactions);
        AccountHolderSimpleModel GetAccountHolderSimple(int id);
        AccountHolderSimpleModel GetByName(string name);
        AccountHolder UpdateAccountHolder(int id, AccountHolderUpdateModel accountHolder);
    }
}
