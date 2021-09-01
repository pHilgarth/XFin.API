using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class ExternalTransactionRepository : IExternalTransactionRepository
    {
        public ExternalTransactionRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ExternalTransaction CreateExternalTransaction(ExternalTransactionCreationModel transaction)
        {
            var newTransaction = mapper.Map<ExternalTransaction>(transaction);
            newTransaction.Date = DateTime.Parse(transaction.DateString);
            newTransaction.CounterPartTransactionToken = Guid.NewGuid().ToString();
            newTransaction.TransactionToken = Guid.NewGuid().ToString();
            //var newTransaction = new ExternalTransaction
            //{
            //    ExternalBankAccountId = transaction.ExternalBankAccountId,
            //    Amount = transaction.Amount,
            //    Reference = transaction.Reference,
            //};

            context.ExternalTransactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            newTransaction.ExternalBankAccount = null;

            return newTransaction;
        }

        private XFinDbContext context;
        private readonly IMapper mapper;
    }
}
