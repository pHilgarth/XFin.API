using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IBankAccountRepository
    {
        BankAccountModel GetBankAccount(int id, bool includeTransactions, int year, int month);
    }
}
