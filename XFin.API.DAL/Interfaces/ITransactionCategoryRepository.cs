using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        TransactionCategory CreateTransactionCategory(TransactionCategoryCreationModel transactionCategory);
        List<TransactionCategorySimpleModel> GetAll();
        List<TransactionCategoryModel> GetAllByAccount(int id, int year, int month);
        TransactionCategory Update(int id, JsonPatchDocument<TransactionCategoryUpdateModel> transactionCategoryPatch);
    }
}
