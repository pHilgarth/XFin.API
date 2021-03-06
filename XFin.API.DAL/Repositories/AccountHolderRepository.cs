using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        public AccountHolderRepository(XFinDbContext context)
        {
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

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            foreach (var accountHolder in accountHolders)
            {
                var bankAccounts = context.BankAccounts.Where(b => b.AccountHolderId == accountHolder.Id)
                    .Include(b => b.BankAccountIdentifier)
                    .Include(b => b.Transactions);

                foreach (var bankAccount in bankAccounts)
                {
                    accountHolder.BankAccounts.Add(new BankAccountModel
                    {
                        Id = bankAccount.Id,
                        AccountHolderId = bankAccount.AccountHolderId,
                        Balance = CalculateBalance(bankAccount, currentYear, currentMonth),
                        AccountNumber = CalculateAccountNumber(bankAccount.BankAccountIdentifierIban),
                        Iban = bankAccount.BankAccountIdentifierIban,
                        Bic = bankAccount.BankAccountIdentifier.Bic,
                        Bank = bankAccount.Bank,
                        AccountType = bankAccount.AccountType
                    });
                }
            }

            return accountHolders;

            //    var accountHolderModel = new AccountHolderModel
            //    {
            //        Id              = accountHolder.Id,
            //        Name            = accountHolder.Name,
            //        BankAccounts    = new List<BankAccountModel>()
            //    };

            //    if (includeAccounts)
            //    {
            //        foreach (var bankAccount in accountHolder.BankAccounts)
            //        {
            //            accountHolderModel.BankAccounts.Add(
            //                new BankAccountModel
            //                {
            //                    Id              = bankAccount.Id,
            //                    AccountHolderId     = bankAccount.AccountHolderId,
            //                    Balance         = bankAccount.Balance,
            //                    AccountNumber   = bankAccount.AccountNumber,
            //                    Iban            = bankAccount.Iban,
            //                    Bic             = bankAccount.Bic,
            //                    Bank            = bankAccount.Bank,
            //                    AccountType     = bankAccount.AccountType
            //                });
            //        }
            //    }

            //    accountHolders.Add(accountHolderModel);

            //}

            //return accountHolders.Count != 0 ? accountHolders : null;
        }

        private string CalculateAccountNumber(string iban)
        {
            return iban.Substring(iban.Length - 10).TrimStart('0');
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

        private decimal CalculateBalance(BankAccount bankAccount, int year, int month)
        {

            var revenues = bankAccount.Transactions.Where(
                t => t.Amount > 0m &&
                (t.Date.Year < year || t.Date.Year == year && t.Date.Month <= month));

            var expenses = bankAccount.Transactions.Where(
                t => t.Amount < 0m &&
                (t.Date.Year < year || t.Date.Year == year && t.Date.Month <= month));

            var revenuesTotal = revenues.Select(r => r.Amount).Sum();
            var expensesTotal = Math.Abs(expenses.Select(e => e.Amount).Sum());

            return revenuesTotal - expensesTotal;
        }

        private readonly XFinDbContext context;

    }
}
