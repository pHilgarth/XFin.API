using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class RecurringTransactionRepository : IRecurringTransactionRepository
    {
        public RecurringTransactionRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public RecurringTransactionModel Create(RecurringTransactionCreationModel transaction)
        {
            var newTransaction = mapper.Map<RecurringTransaction>(transaction);
            //TODO - check if I need this DateTime.Parse... i dont think so
            //newTransaction.Date = DateTime.Parse(transaction.DateString);
            //newTransaction.CostCenterId = transaction.CostCenterId > 0 ?
            //    context.CostCenters.Where(t => t.Id == transaction.CostCenterId).FirstOrDefault().Id :
            //    context.CostCenters.Where(t => t.Name == "Nicht zugewiesen").FirstOrDefault().Id;

            //if (newTransaction.Reference != "[Kontoinitialisierung]" && newTransaction.TransactionToken == null)
            //{
            //    newTransaction.TransactionToken = Guid.NewGuid().ToString();
            //    newTransaction.CounterPartTransactionToken = Guid.NewGuid().ToString();
            //}

            //var newTransaction = new RecurringTransaction
            //{
            //    InternalBankAccountId = transaction.InternalBankAccountId,
            //    CostCenter = costCenter,
            //    Date = DateTime.Parse(transaction.DateString),
            //    Amount = transaction.Amount,
            //    Reference = transaction.Reference
            //};

            context.RecurringTransactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            //newTransaction.CostCenter = null;
            //newTransaction.InternalBankAccount = null;

            return mapper.Map<RecurringTransactionModel>(newTransaction);
        }

        public List<RecurringTransactionModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<RecurringTransactionModel> GetAllByAccount(int id)
        {
            throw new NotImplementedException();
        }

        public RecurringTransactionModel Update(int id, JsonPatchDocument<RecurringTransactionUpdateModel> costCenterPatch)
        {
            throw new NotImplementedException();
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}