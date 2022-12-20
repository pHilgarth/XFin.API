using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.Core.Services
{
    public interface ITransactionService
    {
        decimal CalculateBalance(List<TransactionBasicModel> revenues, List<TransactionBasicModel> expenses, int year, int month);

        decimal GetProportionPreviousMonth(List<Transaction> transactions, int year, int month);

        //returns expenses from a certain month
        List<Transaction> GetExpensesInMonth(List<Transaction> transactions, int year, int month, bool _internal);

        //returns expenses up to the specified year and month
        List<Transaction> GetExpensesUpToMonth(List<Transaction> transactions, int year, int month);

        //returns revenues from a certain month
        List<Transaction> GetRevenuesInMonth(List<Transaction> transactions, int year, int month, bool _internal);

        //returns all revenues up to the specified year and month
        List<Transaction> GetRevenuesUpToMonth(List<Transaction> transactions, int year, int month);

        string GetAccountNumber(string iban);
    }
}
