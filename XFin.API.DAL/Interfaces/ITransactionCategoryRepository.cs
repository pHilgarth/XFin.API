using System.Collections.Generic;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        List<TransactionCategorySimpleModel> GetTransactionCategories();
        List<TransactionCategoryModel> GetTransactionCategoriesByBankAccount(int id, int year, int month);
    }
}
