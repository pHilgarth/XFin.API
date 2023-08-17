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
        public CostCenterRepository(ICalculatorService calculator, IMapper mapper, XFinDbContext context)
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
                .Include(c => c.BudgetAllocations)
                .Include(c => c.BudgetDeallocations)
                .Include(c => c.Expenses)
                .Include(c => c.CostCenterAssets.Where(cca => cca.BankAccountId == accountId))
                .ToList();

            var costCenterModels = new List<CostCenterModel>();

            foreach (var costCenter in costCenters)
            {
                var costCenterModel = mapper.Map<CostCenterModel>(costCenter);

                var budgetAllocations = costCenter.BudgetAllocations
                    .Where(b => b.BankAccountId == accountId && b.Executed)
                    .ToList();

                var budgetDeallocations = costCenter.BudgetDeallocations
                    .Where(b => b.BankAccountId == accountId && b.Executed)
                    .ToList();

                var expenses = costCenter.Expenses
                    .Where(e => e.SourceBankAccountId == accountId && e.Executed)
                    .ToList();

                costCenterModel.BalancePreviousMonth = calculator.CalculateBalancePreviousMonth(costCenter.BudgetAllocations, costCenter.BudgetDeallocations, costCenter.Expenses, year, month);
                costCenterModel.AllocationBalanceCurrentMonth = calculator.CalculateAllocationBalance(costCenter.BudgetAllocations, costCenter.BudgetDeallocations, year, month);
                costCenterModel.ExpensesSum = calculator.GetTransactionsInMonth(expenses, year, month).Select(t => t.Amount).Sum();
                costCenterModel.Balance = costCenterModel.BalancePreviousMonth + costCenterModel.AllocationBalanceCurrentMonth - costCenterModel.ExpensesSum;

                var costCenterAssetModels = new List<CostCenterAssetModel>();

                foreach (var costCenterAsset in costCenter.CostCenterAssets)
                {
                    var costCenterAssetModel = mapper.Map<CostCenterAssetModel>(costCenterAsset);

                    var assetBudgetAllocations = costCenterAsset.BudgetAllocations
                        .Where(b => b.Executed)
                        .ToList();

                    var assetBudgetDeallocations = costCenterAsset.BudgetDeallocations
                        .Where(b => b.Executed)
                        .ToList();

                    costCenterAssetModel.BalancePreviousMonth = calculator.CalculateBalancePreviousMonth(costCenterAsset.BudgetAllocations, costCenterAsset.BudgetDeallocations, costCenterAsset.Expenses, year, month);
                    costCenterAssetModel.AllocationBalanceCurrentMonth = calculator.CalculateAllocationBalance(costCenterAsset.BudgetAllocations, costCenterAsset.BudgetDeallocations, year, month);
                    costCenterAssetModel.ExpensesSum = calculator.GetTransactionsInMonth(expenses, year, month).Select(t => t.Amount).Sum();
                    costCenterAssetModel.Balance = costCenterAssetModel.BalancePreviousMonth + costCenterAssetModel.AllocationBalanceCurrentMonth - costCenterAssetModel.ExpensesSum;

                    costCenterAssetModels.Add(costCenterAssetModel);
                }

                costCenterModel.CostCenterAssets = costCenterAssetModels;

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
        private ICalculatorService calculator;
        private XFinDbContext context;
    }
}
