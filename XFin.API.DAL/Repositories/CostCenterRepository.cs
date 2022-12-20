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

        public CostCenter Create(CostCenterCreationModel costCenter)
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
        public List<CostCenterModel> GetAllByAccount(int accountId, int year, int month)
        {
            //var costCenters = mapper.Map<List<CostCenterModel>>(context.CostCenters
            var costCenters = context.CostCenters
                .Include(c => c.CostCenterAssets.Where(ca => ca.BankAccountId == accountId))
                .Include(c => c.Reserves.Where(r => r.BankAccountId == accountId))
                .Include(c => c.Expenses)
                .Include(c => c.Revenues)
                .ToList();

            var costCenterModels = new List<CostCenterModel>();

            foreach (var costCenter in costCenters)
            {
                var costCenterModel = mapper.Map<CostCenterModel>(costCenter);

                //TODO - check if the transactions are properly sorted into expenses and revenues
                costCenter.Expenses = costCenter.Expenses.Where(e => e.SourceBankAccountId == accountId).ToList();
                costCenter.Revenues = costCenter.Revenues.Where(r => r.TargetBankAccountId == accountId).ToList();
                
                var revenues = costCenter.Revenues.Select(r => r.Amount).Sum();
                var expenses = costCenter.Expenses.Select(e => e.Amount).Sum();
                costCenterModel.Amount = costCenter.Revenues.Select(r => r.Amount).Sum() - costCenter.Expenses.Select(e => e.Amount).Sum();

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
