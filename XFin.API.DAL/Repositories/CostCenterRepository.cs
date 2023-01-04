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
    public class CostCenterRepository : ICostCenterRepository
    {
        public CostCenterRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.calculator = calculator;
            this.mapper = mapper;
        }

        public CostCenter Create(CostCenterCreationModel costCenter)
        {
            var newCostCenter = mapper.Map<CostCenter>(costCenter);

            context.CostCenters.Add(newCostCenter);
            context.SaveChanges();

            return newCostCenter;
        }

        public List<CostCenterSimpleModel> GetAllByUser(int userId)
        {
            var costCenters = context.CostCenters
                .Where(c => c.UserId == userId)
                .ToList().OrderBy(t => t.Name);

            return mapper.Map<List<CostCenterSimpleModel>>(costCenters);
        }

        public CostCenterSimpleModel GetSingleByUserAndName(int userId, string name)
        {
            return mapper.Map<CostCenterSimpleModel>(context.CostCenters.Where(c => c.UserId == userId && c.Name == name).FirstOrDefault());
        }

        //TODO - review - include a possibility for NoContent
        public List<CostCenterModel> GetAllByUserAndAccount(int userId, int accountId, int year, int month)
        {
            //var costCenters = mapper.Map<List<CostCenterModel>>(context.CostCenters
            var costCenters = context.CostCenters
                .Where(c => c.UserId == userId)
                .Include(c => c.CostCenterAssets.Where(ca => ca.BankAccountId == accountId))
                .Include(c => c.Reserves.Where(r => r.BankAccountId == accountId))
                .Include(c => c.Expenses)
                .Include(c => c.Revenues)
                .ToList();

            var costCenterModels = new List<CostCenterModel>();

            foreach (var costCenter in costCenters)
            {
                var costCenterModel = mapper.Map<CostCenterModel>(costCenter);

                var revenues = costCenter.Revenues
                    .Where(r => r.TargetBankAccountId == accountId && r.TransactionType != TransactionType.AccountTransfer && r.Executed)
                    .ToList();

                var expenses = costCenter.Expenses
                    .Where(e => e.SourceBankAccountId == accountId && e.TransactionType != TransactionType.AccountTransfer && e.Executed)
                    .ToList();

                var transferRevenues = costCenter.Revenues
                    .Where(r => r.TargetBankAccountId == accountId && r.TransactionType == TransactionType.AccountTransfer && r.Executed)
                    .ToList();

                var transferExpenses = costCenter.Expenses
                    .Where(e => e.TargetBankAccountId == accountId && e.TransactionType == TransactionType.AccountTransfer && e.Executed)
                    .ToList();

                costCenterModel.BalancePreviousMonth = calculator.GetBalancePreviousMonth(costCenter.Revenues, costCenter.Expenses, year, month);
                costCenterModel.RevenuesSum = calculator.GetTransactionsInMonth(revenues, year, month).Select(t => t.Amount).Sum();
                costCenterModel.ExpensesSum = calculator.GetTransactionsInMonth(expenses, year, month).Select(t => t.Amount).Sum();
                costCenterModel.TransferSum = transferRevenues.Select(t => t.Amount).Sum() - transferExpenses.Select(t => t.Amount).Sum();
                costCenterModel.Balance = costCenterModel.BalancePreviousMonth + costCenterModel.RevenuesSum + costCenterModel.TransferSum - costCenterModel.ExpensesSum;

                var reserveModels = new List<ReserveSimpleModel>();

                foreach (var reserve in costCenter.Reserves)
                {
                    var reserveModel = mapper.Map<ReserveSimpleModel>(reserve);

                    //balance = revenues - expenses
                    reserveModel.Balance =
                        reserve.Transactions.Where(t => t.TargetBankAccountId == reserve.BankAccountId && t.TargetCostCenterId == reserve.CostCenterId).Select(t => t.Amount).Sum() -
                        reserve.Transactions.Where(t => t.SourceBankAccountId == reserve.BankAccountId && t.SourceCostCenterId == reserve.CostCenterId).Select(t => t.Amount).Sum();

                    reserveModels.Add(reserveModel);
                }

                costCenterModel.Reserves = reserveModels;

                costCenterModels.Add(costCenterModel);
            }

            return costCenterModels.OrderBy(c => c.Name).ToList();
        }

        public CostCenter Update(int id, JsonPatchDocument<CostCenterUpdateModel> costCenterPatch)
        {
            var costCenterEntity = context.CostCenters.Where(t => t.Id == id).FirstOrDefault();

            if (costCenterPatch != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var costCenterTopatch = mapper.Map<CostCenterUpdateModel>(costCenterEntity);

                costCenterPatch.ApplyTo(costCenterTopatch);

                mapper.Map(costCenterTopatch, costCenterEntity);

                context.SaveChanges();

                return costCenterEntity;
            }

            return null;
        }

        private IMapper mapper;
        private ITransactionService calculator;
        private XFinDbContext context;
    }
}
