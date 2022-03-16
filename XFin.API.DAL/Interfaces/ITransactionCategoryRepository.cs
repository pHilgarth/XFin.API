using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        TransactionCategory CreateTransactionCategory(TransactionCategoryCreationModel transactionCategory);
        List<TransactionCategorySimpleModel> GetAll();
        List<TransactionCategoryModel> GetAllByAccount(int id, int year, int month);
    }
}
