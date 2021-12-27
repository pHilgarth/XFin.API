using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

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

        public List<TransactionCategorySimpleModel> GetAll()
        {
            var transactionCategories = context.TransactionCategories.ToList();

            return mapper.Map<List<TransactionCategorySimpleModel>>(transactionCategories);
        }

        //TODO - review - include a possibility for NoContent
        public List<TransactionCategoryModel> GetAllByAccount(int id, int year, int month)
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
                var transactionCategoryModel = mapper.Map<TransactionCategoryModel>(transactionCategory);

                transactionCategory.Transactions = transactionCategory.Transactions.Where(t => t.InternalBankAccountId == id).ToList();

                //TODO - check if prop prev month is calculated correctly (need more data)
                transactionCategoryModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(transactionCategory.Transactions, year, month);

                //account external revenues (from another account or initialization transaction)
                transactionCategoryModel.RevenuesTotal = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, false)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .Select(r => r.Amount).Sum();

                var internalRevenuesTotal = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, true)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .Select(r => r.Amount).Sum();
                //accountInternalExpenses = all expenses from this transactionCategory to another transactionCategory on the same account
                //these are needed to subtract them from the total revenues for this transactionCategory
                var internalExpensesTotal = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, true)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .Select(e => Math.Abs(e.Amount)).Sum();

                //accountExternalExpenses = all expenses from this transactionCategory to another bankAccount or external party
                //these are needed to calculate the total expenses for this transactionCategory
                var externalExpensesTotal = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, false)
                    .Where(t => t.TransactionCategoryId == transactionCategory.Id)
                    .Select(e => Math.Abs(e.Amount)).Sum();



                transactionCategoryModel.InternalTransfersAmount = internalRevenuesTotal - internalExpensesTotal;

                transactionCategoryModel.Budget = transactionCategoryModel.ProportionPreviousMonth + transactionCategoryModel.RevenuesTotal + transactionCategoryModel.InternalTransfersAmount;
                transactionCategoryModel.ExpensesTotal = externalExpensesTotal;
                transactionCategoryModel.Balance = transactionCategoryModel.Budget - transactionCategoryModel.ExpensesTotal;

                transactionCategoryModels.Add(transactionCategoryModel);

                //TODO - here I need to calculate the BlockedBudget!
            }

            return transactionCategoryModels;
        }

        private IMapper mapper;
        private ITransactionService calculator;
        private XFinDbContext context;
    }
}
