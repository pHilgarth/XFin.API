using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Services
{
    public interface ITransactionService
    {
        decimal CalculateBalance(BankAccount bankAccount, int year, int month);

        //returns expenses from a certain month
        ICollection<Transaction> GetExpensesInMonth(ICollection<Transaction> transactions, int year, int month);

        //returns expenses up to the specified year and month
        ICollection<Transaction> GetExpensesUpToMonth(ICollection<Transaction> transactions, int year, int month);

        //returns revenues from a certain month
        ICollection<Transaction> GetRevenuesInMonth(ICollection<Transaction> transactions, int year, int month);

        //returns all revenues up to the specified year and month
        ICollection<Transaction> GetRevenuesUpToMonth(ICollection<Transaction> transactions, int year, int month);
    }
}
