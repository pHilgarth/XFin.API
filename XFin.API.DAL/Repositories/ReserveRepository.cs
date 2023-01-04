using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;
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
                //reserve.BankAccount.Reserves = null;
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
                reserve.Revenues = mapper.Map<List<TransactionBasicModel>>(
                    context.Transactions
                    .Where(t => t.ReserveId == reserve.Id && t.TargetBankAccountId == reserve.BankAccount.Id && t.TransactionType != TransactionType.AccountTransfer)
                    .ToList());

                reserve.TransferRevenues = mapper.Map<List<TransactionBasicModel>>(
                    context.Transactions
                        .Where(t => t.ReserveId == reserve.Id && t.TargetCostCenterId == reserve.CostCenter.Id && t.TransactionType == TransactionType.AccountTransfer)
                        .ToList());

                reserve.Expenses = mapper.Map <List<TransactionBasicModel>>(
                    context.Transactions
                    .Where(t => t.ReserveId == reserve.Id && t.SourceBankAccountId == reserve.BankAccount.Id && t.TransactionType != TransactionType.AccountTransfer)
                    .ToList());

                reserve.TransferExpenses = mapper.Map<List<TransactionBasicModel>>(
                    context.Transactions
                        .Where(t => t.ReserveId == reserve.Id && t.SourceCostCenterId == reserve.CostCenter.Id && t.TransactionType == TransactionType.AccountTransfer)
                        .ToList());

                reserve.BankAccount.AccountHolderName = context.AccountHolders
                    .Where(a => a.Id == reserve.BankAccount.AccountHolderId)
                    .FirstOrDefault().Name;

                //preventing a 500 cycle error
                //reserve.BankAccount.Reserves = null;
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
                //reserve.BankAccount.Reserves = null;
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
                //reserve.BankAccount.Reserves = null;
                reserve.CostCenter.Reserves = null;
            }

            return reserves;
        }

        public ReserveModel Update(int reserveId, JsonPatchDocument<ReserveUpdateModel> reservePatch)
        {
            var reserve = context.Reserves.Where(l => l.Id == reserveId).FirstOrDefault();

            if (reserve != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var reserveToPatch = mapper.Map<ReserveUpdateModel>(reserve);

                reservePatch.ApplyTo(reserveToPatch);

                mapper.Map(reserveToPatch, reserve);

                context.SaveChanges();

                return mapper.Map<ReserveModel>(reserve);
            }

            return null;
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