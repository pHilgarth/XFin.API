using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IRecurringTransactionRepository
    {
        RecurringTransactionModel Create(RecurringTransactionCreationModel costCenter);
        List<RecurringTransactionModel> GetAll();
        List<RecurringTransactionModel> GetAllByAccount(int id);
        RecurringTransactionModel Update(int id, JsonPatchDocument<RecurringTransactionUpdateModel> costCenterPatch);
    }
}
