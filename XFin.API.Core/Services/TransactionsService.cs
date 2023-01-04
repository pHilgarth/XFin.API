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
        public decimal CalculateBalance(List<Transaction> revenues, List<Transaction> expenses, int year, int month)
        {
            return
                GetTransactionsUpToMonth(revenues, year, month).Select(r => r.Amount).Sum() -
                GetTransactionsUpToMonth(expenses, year, month).Select(e => e.Amount).Sum();
        }

        public decimal GetBalancePreviousMonth(List<Transaction> revenues, List<Transaction> expenses, int year, int month)
        {
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

            return CalculateBalance(revenues, expenses, year, month);
        }

        //TODO - maybe I don't need a separate method for this, it's basically just one line
        //returns transactions from a certain month
        public List<Transaction> GetTransactionsInMonth(List<Transaction> transactions, int year, int month)
        {
            return transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();
        }

        //TODO - maybe I don't need a separate method for this, it's basically just one line
        public List<Transaction> GetTransactionsUpToMonth(List<Transaction> transactions, int year, int month)
        {
            return transactions.Where(t => t.Date.Year < year || (t.Date.Year == year && t.Date.Month <= month)).ToList();
        }

        public string GetAccountNumber(string iban)
        {
            return iban == null ? null : Regex.Replace(iban.Substring(12), "^0+", "");
        }
    }
}
