using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.Core.Services
{
    public class TransactionsService : ITransactionService
    {
        public decimal CalculateBalance(List<TransactionModel> revenues, List<TransactionModel> expenses, int year, int month)
        {
            //var revenues = GetRevenuesUpToMonth(transactions, year, month).Select(r => r.Amount).Sum();
            //var expenses = Math.Abs(GetExpensesUpToMonth(transactions, year, month).Select(e => e.Amount).Sum());

            //return revenues - expenses;
            return 0;
        }

        public decimal GetProportionPreviousMonth(List<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            if (month == 1)
            {
                //prev month is december last year, so reduce year by one and set month to december!
                year--;
                month = 12;
            }
            else
            {
                month--;
            }

            return 0;// CalculateBalance(transactions, year, month);
        }

        //returns expenses from a certain month
        //if _internal == true it retrieves these expenses, whose counterPartTransactions are also in the given list of transactions
        //account internal = expenses from one category to another category on the same account
        //category internal = expenses from one account to another account on the same category
        public List<Transaction> GetExpensesInMonth(List<Transaction> transactions, int year, int month, bool _internal)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var transactionIds = transactions
                .Select(t => t.Id).ToHashSet();

            return null;
            //return _internal
            //    ? transactions
            //        .Where(t => t.Amount < 0 &&
            //                    t.Date.Year == year && t.Date.Month == month &&
            //                    transactionTokens.Contains(t.CounterPartTransactionToken))
            //        .ToList()
            //    : transactions
            //        .Where(t => t.Amount < 0 &&
            //                    t.Date.Year == year && t.Date.Month == month &&
            //                    !transactionTokens.Contains(t.CounterPartTransactionToken))
            //        .ToList();
        }

        //returns expenses up to the specified year and month
        public List<Transaction> GetExpensesUpToMonth(List<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var transactionTokens = transactions
                .Select(t => t.Id).ToHashSet();

            var result = transactions.Where(t =>
                t.Amount < 0 &&
                (t.Date.Year < year ||
                t.Date.Year == year && t.Date.Month <= month) //&&
                //(!transactionTokens.Contains(t.CounterPartTransactionToken) || t.CounterPartTransactionToken == null))
                ).ToList();

            return result;
        }

        //returns revenues from a certain month
        public List<Transaction> GetRevenuesInMonth(List<Transaction> transactions, int year, int month, bool _internal)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var transactionTokens = transactions
                .Select(t => t.Id).ToHashSet();

            return null;

            //return _internal
            //    ? transactions
            //        .Where(t => t.Amount > 0 &&
            //                    t.Date.Year == year && t.Date.Month == month &&
            //                    t.TransactionToken != null && transactionTokens.Contains(t.CounterPartTransactionToken))
            //        .ToList()
            //    : transactions
            //        .Where(t => t.Amount > 0 &&
            //                    t.Date.Year == year && t.Date.Month == month &&
            //                    //t.CounterPartTransactionToken == null refers to the account initialization transaction which takes place when the account is created by the user
            //                    (t.CounterPartTransactionToken == null || !transactionTokens.Contains(t.CounterPartTransactionToken)))
            //        .ToList();
        }

        //returns revenues up to the specified year and month
        public List<Transaction> GetRevenuesUpToMonth(List<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var transactionTokens = transactions
                .Select(t => t.Id).ToHashSet();

            var result = transactions.Where(t =>
                t.Amount > 0 &&
                (t.Date.Year < year ||
                t.Date.Year == year && t.Date.Month <= month) //&&
                //(!transactionTokens.Contains(t.CounterPartTransactionToken) || t.CounterPartTransactionToken == null))
                ).ToList();

            return result;
        }

        public string GetAccountNumber(string iban)
        {
            return iban == null ? null : Regex.Replace(iban.Substring(12), "^0+", "");
        }
    }
}
