using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Services
{
    public class TransactionsService : ITransactionService
    {
        public decimal CalculateBalance(ICollection<Transaction> transactions, int year, int month)
        {
            var revenues = GetRevenuesUpToMonth(transactions, year, month).Select(r => r.Amount).Sum();
            var expenses = Math.Abs(GetExpensesUpToMonth(transactions, year, month).Select(e => e.Amount).Sum());

            return revenues - expenses;
        }

        public decimal GetProportionPreviousMonth(ICollection<Transaction> transactions, int year, int month)
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

            return CalculateBalance(transactions, year, month);
        }

        //returns expenses from a certain month
        public ICollection<Transaction> GetExpensesInMonth(ICollection<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            return transactions.Where(t =>
                t.Amount < 0 &&
                t.Date.Year == year && t.Date.Month == month)
                .ToList();
        }

        //returns expenses up to the specified year and month
        public ICollection<Transaction> GetExpensesUpToMonth(ICollection<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var result = transactions.Where(t =>
                t.Amount < 0 &&
                (t.Date.Year < year ||
                t.Date.Year == year && t.Date.Month <= month))
                .ToList();

            return result;
        }

        //returns revenues from a certain month
        public ICollection<Transaction> GetRevenuesInMonth(ICollection<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            return transactions.Where(t =>
                t.Amount >= 0 &&
                t.Date.Year == year && t.Date.Month == month)
                .ToList();
        }

        //returns revenues up to the specified year and month
        public ICollection<Transaction> GetRevenuesUpToMonth(ICollection<Transaction> transactions, int year, int month)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;

            var result = transactions.Where(t =>
                t.Amount > 0 &&
                (t.Date.Year < year ||
                t.Date.Year == year && t.Date.Month <= month))
                .ToList();

            return result;
        }
    }
}
