using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
        public TransactionCategoryRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.calculator = calculator;
            this.mapper = mapper;
        }

        public List<TransactionCategorySimpleModel> GetTransactionCategories()
        {
            var transactionCategories = context.TransactionCategories.ToList();

            return mapper.Map<List<TransactionCategorySimpleModel>>(transactionCategories);
        }

        public List<TransactionCategoryModel> GetTransactionCategoriesByBankAccount(int id, int year, int month)
        {
            var transactionCategoryModels = new List<TransactionCategoryModel>();
            var transactionCategories = context.TransactionCategories
                .Include(t => t.Transactions)
                .ToList();
            var bankAccount = context.InternalBankAccounts
                .Where(b => b.Id == id)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            foreach (var transactionCategory in transactionCategories)
            {
                //accountExternalExpenses = all expenses from this transactionCategory to another bankAccount or external party
                //these are needed to calculate the total expenses for this transactionCategory
                var accountExternalExpenses = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .ToList();

                //accountInternalExpenses = all expenses from this transactionCategory to another transactionCategory on the same account
                //these are needed to subtract them from the total revenues for this transactionCategory
                var accountInternalExpenses = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, true)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .ToList();
                
                transactionCategory.Transactions = transactionCategory.Transactions.Where(t => t.InternalBankAccountId == id).ToList();

                //TODO - I think I dont really need the transactions for transactionCategories - I need them only to calculate thins
                //var revenues = calculator.GetRevenuesInMonth(transactionCategory.Transactions, year, month).ToList();
                //var expenses = calculator.GetExpensesInMonth(transactionCategory.Transactions, year, month).ToList();

                var transactionCategoryModel = mapper.Map<TransactionCategoryModel>(transactionCategory);

                //transactionCategoryModel.Revenues = mapper.Map<List<InternalTransactionModel>>(revenues);
                //transactionCategoryModel.Expenses = mapper.Map<List<InternalTransactionModel>>(expenses);

                //TODO - check if prop prev month is calculated correctly (need more data)
                transactionCategoryModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(transactionCategory.Transactions, year, month);

                //get total revenues and subtract the account internal expenses from it
                //Math.Abs because Amount of expenses is < 0
                transactionCategoryModel.RevenuesTotal = calculator.GetRevenuesInMonth(transactionCategory.Transactions, year, month)
                    .Select(r => r.Amount).Sum() - Math.Abs(accountInternalExpenses.Select(e => e.Amount).Sum());

                transactionCategoryModel.Budget = transactionCategoryModel.ProportionPreviousMonth + transactionCategoryModel.RevenuesTotal;
                transactionCategoryModel.ExpensesTotal = Math.Abs(accountExternalExpenses.Select(e => e.Amount).Sum());
                transactionCategoryModel.Balance = transactionCategoryModel.Budget - transactionCategoryModel.ExpensesTotal;

                transactionCategoryModels.Add(transactionCategoryModel);
            }

            return transactionCategoryModels;
        }

        private IMapper mapper;
        private ITransactionService calculator;
        private XFinDbContext context;
    }
}
