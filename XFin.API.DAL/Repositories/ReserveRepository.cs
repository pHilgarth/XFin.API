using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public class ReserveRepository : IReserveRepository
    {
        public ReserveRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.calculator = calculator;
            this.context = context;
            this.mapper = mapper;
        }

        public Reserve Create(ReserveCreationModel reserve)
        {
            if (reserve.CostCenterId == null)
            {
                reserve.CostCenterId = context.CostCenters
                    .Where(c => c.Name == "Nicht zugewiesen")
                    .FirstOrDefault()
                    .Id;
            }

            var newReserve = mapper.Map<Reserve>(reserve);

            context.Reserves.Add(newReserve);
            context.SaveChanges();

            return newReserve;
        }

        public List<ReserveModel> GetAll()
        {
            var reserves = mapper.Map<List<ReserveModel>>(
                context.Reserves
                .Include(r => r.BankAccount)
                .Include(r => r.CostCenter)
                .ToList());

            foreach(var reserve in reserves)
            {
                //preventing a 500 cycle error
                reserve.BankAccount.Reserves = null;
                reserve.CostCenter.Reserves = null;
            }

            return reserves;
        }

        public List<ReserveModel> GetAllByAccount(int accountId)
        {
            var reserves = mapper.Map<List<ReserveModel>>(
                context.Reserves
                .Where(r => r.BankAccountId == accountId)
                .Include(r => r.BankAccount)
                .Include(r => r.CostCenter)
                .ToList());

            foreach(var reserve in reserves)
            {
                reserve.Revenues = mapper.Map<List<TransactionModel>>(
                    context.Transactions
                    .Where(t => t.ReserveId == reserve.Id && t.TargetBankAccountId == reserve.BankAccount.Id)
                    .ToList());

                reserve.Expenses = mapper.Map <List<TransactionModel>>(
                    context.Transactions
                    .Where(t => t.ReserveId == reserve.Id && t.SourceBankAccountId == reserve.BankAccount.Id)
                    .ToList());

                //preventing a 500 cycle error
                reserve.BankAccount.Reserves = null;
                reserve.CostCenter.Reserves = null;
            }

            return reserves;
        }

        public List<ReserveModel> GetAllByAccountAndCostCenter(int accountId, int costCenterId)
        {
            var reserves = mapper.Map<List<ReserveModel>>(
                context.Reserves
                .Where(r => r.BankAccountId == accountId && r.CostCenterId == costCenterId)
                .Include(r => r.BankAccount)
                .Include(r => r.CostCenter)
                .ToList());

            foreach (var reserve in reserves)
            {
                //preventing a 500 cycle error
                reserve.BankAccount.Reserves = null;
                reserve.CostCenter.Reserves = null;
            }

            return reserves;
        }

        public List<ReserveModel> GetAllByCostCenter(int costCenterId)
        {
            var reserves = mapper.Map<List<ReserveModel>>(
                context.Reserves
                .Where(r => r.CostCenterId == costCenterId)
                .Include(r => r.CostCenter)
                .Include(r => r.BankAccount)
                .ToList());

            foreach(var reserve in reserves)
            {
                //preventing a 500 cycle error
                reserve.BankAccount.Reserves = null;
                reserve.CostCenter.Reserves = null;
            }

            return reserves;
        }

        //public List<ReserveSimpleModel> GetReversesSimple()
        //{
        //    throw new NotImplementedException();
        //}

        //public ReserveModel GetReserve(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public ReserveSimpleModel GetReserveSimple(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Reserve Update(int id, JsonPatchDocument<ReserveUpdateModel> reservePatch)
        //{
        //    throw new NotImplementedException();
        //}

        private readonly ITransactionService calculator;
        private readonly IMapper mapper;
        private readonly XFinDbContext context;
    }
}