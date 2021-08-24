using AutoMapper;
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
        public TransactionCategoryRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.calculator = calculator;
            this.mapper = mapper;
        }

        public List<TransactionCategoryModel> GetTransactionCategoriesByBankAccount(int id, bool includeTransactions, int year, int month)
        {
            var transactionCategories = context.TransactionCategories
                .Include(t => t.Transactions)
                .ToList();

            var transactionCategoryModels = new List<TransactionCategoryModel>();

            foreach (var transactionCategory in transactionCategories)
            {
                transactionCategory.Transactions = transactionCategory.Transactions.Where(t => t.InternalBankAccountId == id).ToList();

                var proportionPreviousMonth = calculator.GetProportionPreviousMonth(transactionCategory.Transactions, year, month);
                var revenuesTotal = calculator.GetRevenuesInMonth(transactionCategory.Transactions, year, month).Select(r => r.Amount).Sum();
                var budget = proportionPreviousMonth + revenuesTotal;

                var transactionCategoryModel = mapper.Map<TransactionCategoryModel>(transactionCategory);
                transactionCategoryModel.ProportionPreviousMonth = proportionPreviousMonth;
                transactionCategoryModel.RevenuesTotal = revenuesTotal;
                transactionCategoryModel.Budget = budget;
            }

            if (includeTransactions)
            {
                //nothing here yet
            }

            return transactionCategoryModels;
        }

        private IMapper mapper;
        private ITransactionService calculator;
        private XFinDbContext context;
    }
}
