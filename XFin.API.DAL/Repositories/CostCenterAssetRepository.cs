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
    public class CostCenterAssetRepository : ICostCenterAssetRepository
    {
        public CostCenterAssetRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.calculator = calculator;
            this.mapper = mapper;
        }

        public CostCenterAsset Create(CostCenterAssetCreationModel costCenterAsset)
        {
            var newCostCenterAsset = mapper.Map<CostCenterAsset>(costCenterAsset);

            context.CostCenterAssets.Add(newCostCenterAsset);
            context.SaveChanges();

            return newCostCenterAsset;
        }

        //TODO - review - include a possibility for NoContent
        public List<CostCenterAssetModel> GetAllByAccountAndCostCenter(int accountId, int costCenterId)
        {
            var costCenterAssets = mapper.Map<List<CostCenterAssetModel>>(
                context.CostCenterAssets
                    .Where(c => c.BankAccountId == accountId && c.CostCenterId == costCenterId)
                    .ToList());

            return costCenterAssets;
        }

        public CostCenterAssetModel Update(int id, JsonPatchDocument<CostCenterAssetUpdateModel> costCenterAssetPatch)
        {
            var costCenterAsset = context.CostCenterAssets.Where(c => c.Id == id).FirstOrDefault();

            if (costCenterAsset != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var costCenterAssetToPatch = mapper.Map<CostCenterAssetUpdateModel>(costCenterAsset);

                costCenterAssetPatch.ApplyTo(costCenterAssetToPatch);

                mapper.Map(costCenterAssetToPatch, costCenterAsset);

                context.SaveChanges();

                return mapper.Map<CostCenterAssetModel>(costCenterAsset);
            }

            return null;
        }

        //public CostCenterAsset Update(int id, JsonPatchDocument<CostCenterAssetUpdateModel> costCenterAssetPatch)
        //{
        //    var costCenterAssetEntity = context.CostCenterAssets.Where(t => t.Id == id).FirstOrDefault();

        //    if (costCenterAssetPatch != null)
        //    {
        //        //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
        //        var costCenterAssetTopatch = mapper.Map<CostCenterAssetUpdateModel>(costCenterAssetEntity);

        //        costCenterAssetPatch.ApplyTo(costCenterAssetTopatch);

        //        mapper.Map(costCenterAssetTopatch, costCenterAssetEntity);

        //        context.SaveChanges();

        //        return costCenterAssetEntity;
        //    }

        //    return null;
        //}

        private IMapper mapper;
        private ITransactionService calculator;
        private XFinDbContext context;
    }
}
