using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IBankAccountRepository
    {
        BankAccount CreateBankAccount(BankAccountCreationModel bankAccount);
        BankAccountModel GetBankAccount(string accountNumber, bool includeTransactions, int year, int month);
    }
}
