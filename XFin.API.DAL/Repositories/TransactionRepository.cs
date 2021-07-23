using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public TransactionRepository(XFinDbContext context)
        {
            this.context = context;
        }

        public Transaction CreateTransaction(TransactionCreationModel transaction)
        {
            var transactionCategory = transaction.TransactionCategoryId > 0 ?
                context.TransactionCategories.Where(t => t.Id == transaction.TransactionCategoryId).FirstOrDefault() :
                context.TransactionCategories.Where(t => t.Name == "Nicht zugewiesen").FirstOrDefault();

            var newTransaction = new Transaction
            {
                BankAccountAccountNumber = transaction.BankAccountNumber,
                TransactionCategory = transactionCategory,
                Date = DateTime.Parse(transaction.DateString),
                Amount = transaction.Amount,
                Reference = transaction.Reference
            };

            context.Transactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            newTransaction.TransactionCategory = null;

            return newTransaction;
        }

        XFinDbContext context;
    }
}
