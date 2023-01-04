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

        public TransactionBasicModel Create(TransactionCreationModel transaction)
        {
            var newTransaction = mapper.Map<Transaction>(transaction);

            newTransaction.DueDate = DateTime.Parse(transaction.DueDateString);
            newTransaction.Date = DateTime.Parse(transaction.DateString);

            context.Transactions.Add(newTransaction);
            context.SaveChanges();

            return mapper.Map<TransactionBasicModel>(newTransaction);
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}