using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;
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
            if (transaction.TargetCostCenterId == null)
            {
                transaction.TargetCostCenterId = context.CostCenters
                    .Where(c => c.Name == "Nicht zugewiesen")
                    .FirstOrDefault()
                    .Id;
            }

            if (transaction.TransactionType == null)
            {
                transaction.TransactionType = "Default";
            }

            var newTransaction = mapper.Map<Transaction>(transaction);

            newTransaction.Date = DateTime.Parse(transaction.DateString);

            context.Transactions.Add(newTransaction);
            context.SaveChanges();

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