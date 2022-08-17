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

        public AccountHolder Create(AccountHolderCreationModel accountHolder)
        {
            var newAccountHolder = mapper.Map<AccountHolder>(accountHolder);

            context.AccountHolders.Add(newAccountHolder);
            context.SaveChanges();

            return newAccountHolder;
        }

        public List<AccountHolderModel> GetAllByUser(int userId)
        {
            var accountHolders = mapper.Map<List<AccountHolderModel>>(
                context.AccountHolders
                    .Where(a => a.UserId == userId)
                    .Include(a => a.BankAccounts).ThenInclude(b => b.Revenues.OrderByDescending(b => b.Date))
                    .Include(a => a.BankAccounts).ThenInclude(b => b.Expenses.OrderByDescending(b => b.Date))
                );

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            foreach (var accountHolder in accountHolders)
            {
                foreach (var bankAccount in accountHolder.BankAccounts)
                {
                    //var revenues = calculator.GetRevenuesInMonth(bankAccount.Transactions, 0, 0, false);
                    //var expenses = calculator.GetExpensesInMonth(bankAccount.Transactions, 0, 0, false);

                    bankAccount.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);
                    //bankAccountModel.Revenues = mapper.Map <List<TransactionModel>>(revenues);
                    //bankAccountModel.Expenses = mapper.Map <List<TransactionModel>>(expenses);
                    //bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth);
                }
            }

            return accountHolders;
        }

        public AccountHolderModel GetSingle(int accountHolderId)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Id == accountHolderId).FirstOrDefault();

            if (accountHolder != null)
            {
                var accountHolderModel = mapper.Map<AccountHolderModel>(accountHolder);

                var bankAccounts = context.BankAccounts
                    .Where(b => b.AccountHolderId == accountHolderId)
                    .Include(b => b.Revenues)
                    .Include(b => b.Expenses)
                    .ToList();

                foreach (var bankAccount in bankAccounts)
                {
                    var currentYear = DateTime.Now.Year;
                    var currentMonth = DateTime.Now.Month;

                    var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);

                    //bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth);
                    bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                    accountHolderModel.BankAccounts.Add(bankAccountModel);
                }

                return accountHolderModel;
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