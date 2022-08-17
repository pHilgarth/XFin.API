using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public TransactionRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TransactionModel Create(TransactionCreationModel transaction)
        {
            var newTransaction = mapper.Map<Transaction>(transaction);
            ////TODO - check if I need this DateTime.Parse... i dont think so
            //newTransaction.Date = DateTime.Parse(transaction.DateString);
            //newTransaction.CostCenterId = transaction.CostCenterId > 0 ?
            //    context.CostCenters.Where(t => t.Id == transaction.CostCenterId).FirstOrDefault().Id :
            //    context.CostCenters.Where(t => t.Name == "Nicht zugewiesen").FirstOrDefault().Id;

            //if (newTransaction.Reference != "[Kontoinitialisierung]" && newTransaction.TransactionToken == null)
            //{
            //    newTransaction.TransactionToken = Guid.NewGuid().ToString();
            //    newTransaction.CounterPartTransactionToken = Guid.NewGuid().ToString();
            //}

            //var newTransaction = new Transaction
            //{
            //    InternalBankAccountId = transaction.InternalBankAccountId,
            //    CostCenter = costCenter,
            //    Date = DateTime.Parse(transaction.DateString),
            //    Amount = transaction.Amount,
            //    Reference = transaction.Reference
            //};

            context.Transactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            //newTransaction.CostCenter = null;
            //newTransaction.InternalBankAccount = null;

            return mapper.Map<TransactionModel>(newTransaction);
        }

        public List<TransactionModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TransactionModel> GetAllByAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}