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
    public class CostCenterRepository : ICostCenterRepository
    {
        public CostCenterRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.calculator = calculator;
            this.mapper = mapper;
        }

        public CostCenter CreateCostCenter(CostCenterCreationModel costCenter)
        {
            var newCostCenter = mapper.Map<CostCenter>(costCenter);

            context.CostCenters.Add(newCostCenter);
            context.SaveChanges();

            return newCostCenter;
        }

        public List<CostCenterSimpleModel> GetAll()
        {
            var costCenters = context.CostCenters.ToList().OrderBy(t => t.Name);

            return mapper.Map<List<CostCenterSimpleModel>>(costCenters);
        }

        //TODO - review - include a possibility for NoContent
        public List<CostCenterModel> GetAllByAccount(int id, int year, int month)
        {
            var costCenterModels = new List<CostCenterModel>();
            var costCenters = context.CostCenters
                .Include(t => t.Transactions)
                .ToList();
            var bankAccount = context.InternalBankAccounts
                .Where(b => b.Id == id)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            foreach (var costCenter in costCenters)
            {
                var costCenterModel = mapper.Map<CostCenterModel>(costCenter);

                costCenter.Transactions = costCenter.Transactions.Where(t => t.InternalBankAccountId == id).ToList();

                //TODO - check if prop prev month is calculated correctly (need more data)
                costCenterModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(costCenter.Transactions, year, month);

                //account external revenues (from another account or initialization transaction)
                costCenterModel.RevenuesTotal = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, false)
                    .Where(t => t.CostCenterId == costCenter.Id)
                    .Select(r => r.Amount).Sum();

                var internalRevenuesTotal = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, true)
                    .Where(t => t.CostCenterId == costCenter.Id)
                    .Select(r => r.Amount).Sum();
                //accountInternalExpenses = all expenses from this costCenter to another costCenter on the same account
                //these are needed to subtract them from the total revenues for this costCenter
                var internalExpensesTotal = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, true)
                    .Where(t => t.CostCenterId == costCenter.Id)
                    .Select(e => Math.Abs(e.Amount)).Sum();

                //accountExternalExpenses = all expenses from this costCenter to another bankAccount or external party
                //these are needed to calculate the total expenses for this costCenter
                var externalExpensesTotal = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, false)
                    .Where(t => t.CostCenterId == costCenter.Id)
                    .Select(e => Math.Abs(e.Amount)).Sum();



                costCenterModel.InternalTransfersAmount = internalRevenuesTotal - internalExpensesTotal;

                costCenterModel.Budget = costCenterModel.ProportionPreviousMonth + costCenterModel.RevenuesTotal + costCenterModel.InternalTransfersAmount;
                costCenterModel.ExpensesTotal = externalExpensesTotal;
                costCenterModel.Balance = costCenterModel.Budget - costCenterModel.ExpensesTotal;

                costCenterModels.Add(costCenterModel);
            }

            return costCenterModels;
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
