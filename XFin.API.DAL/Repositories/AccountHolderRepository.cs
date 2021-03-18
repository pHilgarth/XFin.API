﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        public AccountHolderRepository(ITransactionService calculator, XFinDbContext context)
        {
            this.calculator = calculator;
            this.context = context;
        }

        public List<AccountHolderModel> GetAccountHolders(bool includeAccounts)
        {
            var accountHolders = new List<AccountHolderModel>();

            foreach (var accountHolder in context.AccountHolders)
            {
                var accountHolderModel = new AccountHolderModel
                {
                    Id = accountHolder.Id,
                    Name = accountHolder.Name,
                    BankAccounts = new List<BankAccountModel>()
                };

                accountHolders.Add(accountHolderModel);
            }

            if (includeAccounts)
            {
                var currentYear = DateTime.Now.Year;
                var currentMonth = DateTime.Now.Month;

                foreach (var accountHolder in accountHolders)
                {
                    var bankAccounts = context.BankAccounts.Where(b => b.AccountHolderId == accountHolder.Id)
                        .Include(b => b.BankAccountIdentifier)
                        .Include(b => b.Transactions);

                    foreach (var bankAccount in bankAccounts)
                    {
                        var iban = bankAccount.BankAccountIdentifierIban;

                        accountHolder.BankAccounts.Add(new BankAccountModel
                        {
                            AccountNumber = bankAccount.AccountNumber,
                            AccountHolderId = bankAccount.AccountHolderId,
                            Balance = calculator.CalculateBalance(bankAccount, currentYear, currentMonth),
                            Iban = iban,
                            Bic = bankAccount.BankAccountIdentifier.Bic,
                            Bank = bankAccount.Bank,
                            AccountType = bankAccount.AccountType
                        });
                    }
                }
            }

            return accountHolders;
        }

        public AccountHolderModel GetAccountHolder(int id, bool includeAccounts)
        {
            return new AccountHolderModel();
            //var accountHolder = context.AccountHolders.Where(d => d.Id == id).Include(d => d.BankAccounts).FirstOrDefault();

            //if (accountHolder != null)
            //{
            //    var accountHolderModel = new AccountHolderModel
            //    {
            //        Id = accountHolder.Id,
            //        Name = accountHolder.Name,
            //        BankAccounts = new List<BankAccountModel>()
            //    };

            //    if (includeAccounts)
            //    {
            //        foreach (var bankAccount in accountHolder.BankAccounts)
            //        {
            //            accountHolderModel.BankAccounts.Add(
            //                new BankAccountModel
            //                {
            //                    Id = bankAccount.Id,
            //                    AccountHolderId = bankAccount.AccountHolderId,
            //                    Balance = bankAccount.Balance,
            //                    AccountNumber = bankAccount.AccountNumber,
            //                    Iban = bankAccount.Iban,
            //                    Bic = bankAccount.Bic,
            //                    Bank = bankAccount.Bank,
            //                    AccountType = bankAccount.AccountType
            //                });
            //        }
            //    }

            //    return accountHolderModel;
            //}
            //else
            //{
            //    return null;
            //}
        }

        private readonly ITransactionService calculator;
        private readonly XFinDbContext context;

    }
}
