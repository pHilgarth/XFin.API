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
    public class BudgetAllocationRepository : IBudgetAllocationRepository
    {
        public BudgetAllocationRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BudgetAllocationBasicModel Create(BudgetAllocationCreationModel budgetAllocation)
        {
            //TODO - implement checks for the ids on every endpoint (sourceCostCenter, targetCostCenter, etc.... if there are no ids in the database, an error occurs)
            var newBudgetAllocation = mapper.Map<BudgetAllocation>(budgetAllocation);

            newBudgetAllocation.DueDate = DateTime.Parse(budgetAllocation.DueDateString);
            newBudgetAllocation.Date = DateTime.Parse(budgetAllocation.DateString);

            context.BudgetAllocations.Add(newBudgetAllocation);
            context.SaveChanges();

            return mapper.Map<BudgetAllocationBasicModel>(newBudgetAllocation);
        }

        XFinDbContext context;
        private readonly IMapper mapper;
    }
}