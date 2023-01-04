using System.Collections.Generic;
using XFin.API.Core.Entities;
namespace XFin.API.Core.Services
{
    public interface ITransactionService
    {
        decimal CalculateBalance(List<Transaction> revenues, List<Transaction> expenses, int year, int month);

        decimal GetBalancePreviousMonth(List<Transaction> revenues, List<Transaction> expenses, int year, int month);

        //returns transactions from a certain month
        List<Transaction> GetTransactionsInMonth(List<Transaction> transactions, int year, int month);

        //returns transactions up to the specified year and month
        List<Transaction> GetTransactionsUpToMonth(List<Transaction> transactions, int year, int month);

        string GetAccountNumber(string iban);
    }
}
