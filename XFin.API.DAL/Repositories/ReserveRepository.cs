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
            var newReserve = mapper.Map<Reserve>(reserve);

            context.Reserves.Add(newReserve);
            context.SaveChanges();

            return newReserve;
        }

        public List<ReserveModel> GetAllByAccount(int accountId)
        {
            return mapper.Map<List<ReserveModel>>(
                context.Reserves
                //.Where(r => r.BankAccounttId == accountId)
                .ToList());
        }

        private int List<T>(List<Reserve> reserves)
        {
            throw new NotImplementedException();
        }

        public List<ReserveModel> GetAllByAccountHolder(int accountHolderId)
        {
            var bankAccountIds = context.BankAccounts.
                Where(b => b.AccountHolderId == accountHolderId).
                Select(b => b.Id).
                ToList();

            var reserves = context.Reserves.
                Where(r => bankAccountIds.Contains(r.BankAccountId)).
                Include(r => r.Transactions).
                ToList();

            var reserveModels = new List<ReserveModel>();

            foreach(var reserve in reserves)
            {
                var reserveModel = mapper.Map<ReserveModel>(reserve);
                //reserveModel.Amount = reserve.Transactions.Sum(t => t.Amount);

                reserveModels.Add(reserveModel);
            }

            return reserveModels;
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