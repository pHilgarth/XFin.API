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

        public AccountHolder CreateAccountHolder(AccountHolderCreationModel accountHolder)
        {
            var newAccountHolder = mapper.Map<AccountHolder>(accountHolder);

            context.AccountHolders.Add(newAccountHolder);
            context.SaveChanges();

            return newAccountHolder;
        }

        public List<AccountHolderModel> GetAccountHolders()
        {
            var accountHolders = mapper.Map<List<AccountHolderModel>>(context.AccountHolders);

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            foreach (var accountHolder in accountHolders)
            {
                var bankAccounts = context.InternalBankAccounts
                    .Where(b => b.AccountHolderId == accountHolder.Id)
                    .Include(b => b.Transactions.OrderByDescending(b => b.Date));


                foreach (var bankAccount in bankAccounts)
                {
                    var bankAccountModel = mapper.Map<InternalBankAccountSimpleModel>(bankAccount);

                    bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);
                    bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth);

                    accountHolder.BankAccounts.Add(bankAccountModel);
                }
            }

            return accountHolders;
        }

        public List<AccountHolderSimpleModel> GetAccountHoldersSimple()
        {
            return mapper.Map<List<AccountHolderSimpleModel>>(context.AccountHolders);
        }

        public AccountHolderModel GetAccountHolder(int id, bool simpleAccounts)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Id == id).FirstOrDefault();

            if (accountHolder != null)
            {
                var accountHolderModel = mapper.Map<AccountHolderModel>(accountHolder);

                var bankAccounts = context.InternalBankAccounts
                    .Where(b => b.AccountHolderId == id)
                    .Include(b => b.Transactions)
                    .ToList();

                foreach (var bankAccount in bankAccounts)
                {
                    var currentYear = DateTime.Now.Year;
                    var currentMonth = DateTime.Now.Month;

                    if (simpleAccounts)
                    {
                        var bankAccountModel = mapper.Map<InternalBankAccountSimpleModel>(bankAccount);

                        bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth);
                        bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                        accountHolderModel.BankAccounts.Add(bankAccountModel);
                    }
                    else
                    {
                        var bankAccountModel = mapper.Map<InternalBankAccountModel>(bankAccount);
                        var revenues = calculator.GetRevenuesInMonth(bankAccount.Transactions, currentYear, currentMonth);
                        var expenses = calculator.GetExpensesInMonth(bankAccount.Transactions, currentYear, currentMonth);

                        bankAccountModel.Revenues = mapper.Map<List<InternalTransactionModel>>(revenues);
                        bankAccountModel.Expenses = mapper.Map<List<InternalTransactionModel>>(expenses);
                        bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth);
                        bankAccountModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(bankAccount.Transactions, currentYear, currentMonth);

                        accountHolderModel.BankAccounts.Add(bankAccountModel);
                    }
                }

                return accountHolderModel;
            }

            return null;
        }

        public AccountHolderSimpleModel GetAccountHolderSimple(int id)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Id == id).FirstOrDefault();
            var accountHolderModel = mapper.Map<AccountHolderSimpleModel>(accountHolder);

            return accountHolderModel != null ? accountHolderModel : null;
        }

        public AccountHolderSimpleModel GetByName(string name)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Name == name).FirstOrDefault();
            var accountHolderModel = mapper.Map<AccountHolderSimpleModel>(accountHolder);

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