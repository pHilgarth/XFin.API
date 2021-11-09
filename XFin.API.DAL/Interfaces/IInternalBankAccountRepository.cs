using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IInternalBankAccountRepository
    {
        InternalBankAccount CreateBankAccount(InternalBankAccountCreationModel bankAccount);
        InternalBankAccountModel GetBankAccount(int id, int year, int month);
        InternalBankAccountSimpleModel GetByIban(string iban);
        InternalBankAccountSimpleModel GetBankAccountSimple(int id, int year, int month);
        InternalBankAccount UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch);
    }
}
