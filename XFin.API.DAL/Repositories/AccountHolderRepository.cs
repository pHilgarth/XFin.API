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
    public class AccountHolderRepository : IAccountHolderRepository
    {
        public AccountHolderRepository(ITransactionService calculator, IMapper mapper, XFinDbContext context)
        {
            this.calculator = calculator;
            this.context = context;
            this.mapper = mapper;
        }

        public AccountHolderModel Create(AccountHolderCreationModel accountHolder)
        {
            if (context.Users.Where(u => u.Id == accountHolder.UserId).FirstOrDefault() != null)
            {
                var newAccountHolder = mapper.Map<AccountHolder>(accountHolder);

                context.AccountHolders.Add(newAccountHolder);
                context.SaveChanges();

                return mapper.Map<AccountHolderModel>(newAccountHolder);
            }

            return null;
        }

        public List<AccountHolderModel> GetAllByUser(int userId, bool external)
        {
            if (context.Users.Where(u => u.Id == userId).FirstOrDefault() != null)
            {
                //var accountHolders = mapper.Map<List<AccountHolderModel>>(context.AccountHolders
                var accountHolders = context.AccountHolders.Where(a => a.UserId == userId && a.External == external)
                    .Include(a => a.BankAccounts).ThenInclude(b => b.Revenues)
                    .Include(a => a.BankAccounts).ThenInclude(b => b.Expenses)
                    .ToList();

                var accountHolderModels = new List<AccountHolderModel>();

                foreach (var accountHolder in accountHolders)
                {
                    var bankAccountModels = new List<BankAccountModel>();

                    foreach (var bankAccount in accountHolder.BankAccounts)
                    {
                        var accountNumber = calculator.GetAccountNumber(bankAccount.Iban);
                        //TODO - überall, wo Balance berechnet wird, muss die neue Property 'Executed' berücksichtigt werden!!!!
                        var balance = bankAccount.Revenues.Where(r => r.Executed).Sum(r => r.Amount) - bankAccount.Expenses.Where(e => e.Executed).Sum(e => e.Amount);

                        //bankAccount.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);
                        //bankAccount.Balance = bankAccount.Revenues.Sum(r => r.Amount) - bankAccount.Expenses.Sum(e => e.Amount);

                        //prevents 500 object cycle error
                        bankAccount.Revenues = null;
                        bankAccount.Expenses = null;

                        var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);

                        bankAccountModel.AccountNumber = accountNumber;
                        bankAccountModel.Balance = balance;

                        bankAccountModels.Add(bankAccountModel);
                    }

                    accountHolder.BankAccounts = null;

                    var accountHolderModel = mapper.Map<AccountHolderModel>(accountHolder);
                    accountHolderModel.BankAccounts = bankAccountModels;

                    accountHolderModels.Add(accountHolderModel);

                }

                return accountHolderModels;
            }

            return null;
        }

        public AccountHolderModel GetSingle(int accountHolderId)
        {
            var accountHolder = mapper.Map<AccountHolderModel>(context.AccountHolders
                .Where(a => a.Id == accountHolderId)
                .Include(a => a.BankAccounts).ThenInclude(b => b.Revenues)
                .Include(a => a.BankAccounts).ThenInclude(b => b.Expenses)
                .FirstOrDefault());

            if (accountHolder != null)
            {

                foreach (var bankAccount in accountHolder.BankAccounts)
                {
                    bankAccount.Balance = bankAccount.Revenues.Sum(b => b.Amount) - bankAccount.Expenses.Sum(e => e.Amount);
                    bankAccount.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);
                }

                return accountHolder;
            }

            return null;
        }

        public AccountHolderModel GetByName(string name)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Name == name).FirstOrDefault();
            var accountHolderModel = mapper.Map<AccountHolderModel>(accountHolder);

            return accountHolderModel != null ? accountHolderModel : null;
        }

        public AccountHolder Update(int id, JsonPatchDocument<AccountHolderUpdateModel> accountHolderPatch)
        {
            var accountHolderEntity = context.AccountHolders.Where(a => a.Id == id).FirstOrDefault();

            if (accountHolderEntity != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var accountHolderToPatch = mapper.Map<AccountHolderUpdateModel>(accountHolderEntity);

                accountHolderPatch.ApplyTo(accountHolderToPatch);

                mapper.Map(accountHolderToPatch, accountHolderEntity);

                context.SaveChanges();

                return accountHolderEntity;
            }

            return null;
        }

        private readonly ITransactionService calculator;
        private readonly IMapper mapper;
        private readonly XFinDbContext context;

    }
}