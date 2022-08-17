using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IBankAccountRepository
    {
        BankAccountModel Create(BankAccountCreationModel bankAccount);
        List<BankAccountModel> GetAll();
        BankAccountModel GetSingle(int id, int year, int month);
        BankAccountModel GetByIban(string iban);
        BankAccountModel Update(int id, JsonPatchDocument<BankAccountUpdateModel> bankAccountPatch);
    }
}
