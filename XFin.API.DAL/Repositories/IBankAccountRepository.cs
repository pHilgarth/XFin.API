using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IBankAccountRepository
    {
        InternalBankAccount CreateBankAccount(InternalBankAccountCreationModel bankAccount);
        InternalBankAccountModel GetBankAccount(int id, int year, int month);
        InternalBankAccountSimpleModel GetBankAccountSimple(int id, int year, int month);
        InternalBankAccount UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch);
    }
}
