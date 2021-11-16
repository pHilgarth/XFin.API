using System.Collections.Generic;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        List<TransactionCategorySimpleModel> GetAll();
        List<TransactionCategoryModel> GetAllByAccount(int id, int year, int month);
        List<TransactionCategorySimpleModel> GetAllSimpleByAccount(int id);

    }
}
