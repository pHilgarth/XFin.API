﻿using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Services
{
    public interface ITransactionService
    {
        decimal CalculateBalance(List<InternalTransaction> transactions, int year, int month);

        decimal GetProportionPreviousMonth(List<InternalTransaction> transactions, int year, int month);

        //returns expenses from a certain month
        List<InternalTransaction> GetExpensesInMonth(List<InternalTransaction> transactions, int year, int month, bool _internal);

        //returns expenses up to the specified year and month
        List<InternalTransaction> GetExpensesUpToMonth(List<InternalTransaction> transactions, int year, int month);

        //returns revenues from a certain month
        List<InternalTransaction> GetRevenuesInMonth(List<InternalTransaction> transactions, int year, int month, bool _internal);

        //returns all revenues up to the specified year and month
        List<InternalTransaction> GetRevenuesUpToMonth(List<InternalTransaction> transactions, int year, int month);

        string GetAccountNumber(string iban);
    }
}
