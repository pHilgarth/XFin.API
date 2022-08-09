using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IInternalBankAccountRepository
    {
        InternalBankAccount CreateBankAccount(InternalBankAccountCreationModel bankAccount);
        List<InternalBankAccountModel> GetAll();
        InternalBankAccountModel GetBankAccount(int id, int year, int month);
        InternalBankAccountModel GetByIban(string iban);
        InternalBankAccountModel GetBankAccountSimple(int id, int year, int month);
        InternalBankAccount UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch);
    }
}
