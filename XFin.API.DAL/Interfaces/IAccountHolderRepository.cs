using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IAccountHolderRepository
    {
        AccountHolder Create(AccountHolderCreationModel accountHolder);
        List<AccountHolderModel> GetAllByUser(int userId);
        AccountHolderModel GetSingle(int userId, int accountHolderId);
        AccountHolderModel GetByName(string accountHolderName);
        AccountHolder Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch);
    }
}
