using AutoMapper;
using System;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

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
            //TODO - check if I need this DateTime.Parse... i dont think so
            newTransaction.Date = DateTime.Parse(transaction.DateString);
            newTransaction.CostCenterId = transaction.CostCenterId > 0 ?
                context.CostCenters.Where(t => t.Id == transaction.CostCenterId).FirstOrDefault().Id :
                context.CostCenters.Where(t => t.Name == "Nicht zugewiesen").FirstOrDefault().Id;

            if (newTransaction.Reference != "[Kontoinitialisierung]" && newTransaction.TransactionToken == null)
            {
                newTransaction.TransactionToken = Guid.NewGuid().ToString();
                newTransaction.CounterPartTransactionToken = Guid.NewGuid().ToString();
            }

            //var newTransaction = new InternalTransaction
            //{
            //    InternalBankAccountId = transaction.InternalBankAccountId,
            //    CostCenter = costCenter,
            //    Date = DateTime.Parse(transaction.DateString),
            //    Amount = transaction.Amount,
            //    Reference = transaction.Reference
            //};

            context.InternalTransactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            newTransaction.CostCenter = null;
            newTransaction.InternalBankAccount = null;

            return newTransaction;
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}