using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(ITransactionService transactionService, XFinDbContext context)
        {
            this.context = context;
            this.transactionService = transactionService;
        }

        public List<TransactionCategoryModel> GetTransactionCategoriesByBankAccount(string accountNumber, bool includeTransactions, int year, int month)
        {
            var transactionCategories = context.TransactionCategories
                .Include(t => t.Transactions)
                .ToList();

            foreach (var transactionCategory in transactionCategories)
            {
                transactionCategory.Transactions = transactionCategory.Transactions.Where(t => t.BankAccountAccountNumber == accountNumber).ToList();
            }

            var transactionCategoryModels = new List<TransactionCategoryModel>();

            foreach (var transactionCategory in transactionCategories)
            {
                var proportionPreviousMonth = transactionService.GetProportionPreviousMonth(transactionCategory.Transactions, year, month);
                var revenuesTotal = transactionService.GetRevenuesInMonth(transactionCategory.Transactions, year, month).Select(r => r.Amount).Sum();
                var budget = proportionPreviousMonth + revenuesTotal;

                transactionCategoryModels.Add(new TransactionCategoryModel
                {
                    Id                      = transactionCategory.Id,
                    Name                    = transactionCategory.Name,
                    Balance                 = transactionService.CalculateBalance(transactionCategory.Transactions, year, month),
                    ProportionPreviousMonth = proportionPreviousMonth,
                    RevenuesTotal           = revenuesTotal,
                    Budget                  = budget,
                    ExpensesTotal           = transactionService.GetExpensesInMonth(transactionCategory.Transactions, year, month).Select(r => r.Amount).Sum(),
                    Revenues                = new List<TransactionModel>(),
                    Expenses                = new List<TransactionModel>()
                });
            }

            if (includeTransactions)
            {
                //nothing here yet
            }

            return transactionCategoryModels;
        }

        private ITransactionService transactionService;
        private XFinDbContext context;
    }
}
