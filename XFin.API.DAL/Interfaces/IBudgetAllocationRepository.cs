using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IBudgetAllocationRepository
    {
        BudgetAllocationBasicModel Create(BudgetAllocationCreationModel budgetAllocation);
    }
}
