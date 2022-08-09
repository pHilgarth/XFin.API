using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IAccountHolderRepository
    {
        AccountHolder CreateAccountHolder(AccountHolderCreationModel accountHolder);
        List<AccountHolderModel> GetAccountHolders();
        //AccountHolderModel GetAccountHolder(int id, bool simpleAccounts);
        AccountHolderModel GetAccountHolder(int id);
        AccountHolderModel GetByName(string name);
        AccountHolder Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch);
    }
}
