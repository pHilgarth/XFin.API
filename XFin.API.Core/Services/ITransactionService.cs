using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Services
{
    public interface ITransactionService
    {
        decimal CalculateBalance(ICollection<InternalTransaction> transactions, int year, int month);

        decimal GetProportionPreviousMonth(ICollection<InternalTransaction> transactions, int year, int month);
        //returns expenses from a certain month
        ICollection<InternalTransaction> GetExpensesInMonth(ICollection<InternalTransaction> transactions, int year, int month);

        //returns expenses up to the specified year and month
        ICollection<InternalTransaction> GetExpensesUpToMonth(ICollection<InternalTransaction> transactions, int year, int month);

        //returns revenues from a certain month
        ICollection<InternalTransaction> GetRevenuesInMonth(ICollection<InternalTransaction> transactions, int year, int month);

        //returns all revenues up to the specified year and month
        ICollection<InternalTransaction> GetRevenuesUpToMonth(ICollection<InternalTransaction> transactions, int year, int month);
    }
}
