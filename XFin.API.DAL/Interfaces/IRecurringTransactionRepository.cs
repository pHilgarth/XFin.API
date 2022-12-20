using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IRecurringTransactionRepository
    {
        RecurringTransactionModel Create(RecurringTransactionCreationModel costCenter);
        List<RecurringTransactionModel> GetAllBySourceAccount(int accountId);
        List<RecurringTransactionModel> GetAllByTargetAccount(int accountId);
        List<RecurringTransactionModel> GetAllByDueDate(int year, int month, int day);
        RecurringTransactionModel Update(int id, JsonPatchDocument<RecurringTransactionUpdateModel> recurringTransactionPatch);
    }
}
