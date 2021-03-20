using System.Collections.Generic;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface ITransactionCategoryRepository
    {
        List<TransactionCategoryModel> GetTransactionCategoriesByBankAccount(string accountNumber, bool includeTransactions, int year, int month);
    }
}
