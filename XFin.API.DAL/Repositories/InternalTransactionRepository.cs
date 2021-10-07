using AutoMapper;
using System;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class InternalTransactionRepository : IInternalTransactionRepository
    {
        public InternalTransactionRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public InternalTransaction CreateInternalTransaction(InternalTransactionCreationModel transaction)
        {
            var newTransaction = mapper.Map<InternalTransaction>(transaction);
            newTransaction.Date = DateTime.Parse(transaction.DateString);
            newTransaction.TransactionCategoryId = transaction.TransactionCategoryId > 0 ?
                context.TransactionCategories.Where(t => t.Id == transaction.TransactionCategoryId).FirstOrDefault().Id :
                context.TransactionCategories.Where(t => t.Name == "Nicht zugewiesen").FirstOrDefault().Id;

            if (newTransaction.Reference != "[Kontoinitialisierung]" && newTransaction.TransactionToken == null)
            {
                newTransaction.TransactionToken = Guid.NewGuid().ToString();
                newTransaction.CounterPartTransactionToken = Guid.NewGuid().ToString();
            }

            //var newTransaction = new InternalTransaction
            //{
            //    InternalBankAccountId = transaction.InternalBankAccountId,
            //    TransactionCategory = transactionCategory,
            //    Date = DateTime.Parse(transaction.DateString),
            //    Amount = transaction.Amount,
            //    Reference = transaction.Reference
            //};

            context.InternalTransactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            newTransaction.TransactionCategory = null;
            newTransaction.InternalBankAccount = null;

            return newTransaction;
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}