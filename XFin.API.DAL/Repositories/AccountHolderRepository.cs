using Microsoft.EntityFrameworkCore;
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

        public AccountHolder CreateAccountHolder(AccountHolderCreationModel accountHolder)
        {
            var newAccountHolder = new AccountHolder { Name = accountHolder.Name };

            context.AccountHolders.Add(newAccountHolder);
            context.SaveChanges();

            return newAccountHolder;
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
                            Balance = calculator.CalculateBalance(bankAccount.Transactions, currentYear, currentMonth),
                            Iban = iban,
                            Bic = bankAccount.BankAccountIdentifier.Bic,
                            Bank = bankAccount.Bank,
                            Description = bankAccount.Description
                        });
                    }
                }
            }

            return accountHolders;
        }

        public AccountHolderModel GetAccountHolder(int id, bool includeAccounts)
        {
            var accountHolder = context.AccountHolders.Where(a => a.Id == id).FirstOrDefault();

            if (accountHolder != null)
            {
                var accountHolderModel = new AccountHolderModel
                {
                    Id = accountHolder.Id,
                    Name = accountHolder.Name,
                    BankAccounts = new List<BankAccountModel>()
                };

                if (includeAccounts)
                {
                    var bankAccounts = context.BankAccounts
                        .Where(b => b.AccountHolderId == id)
                        .Include(b => b.BankAccountIdentifier)
                        .ToList();

                    foreach (var bankAccount in bankAccounts)
                    {
                        accountHolderModel.BankAccounts.Add(new BankAccountModel
                        {
                            AccountNumber = bankAccount.AccountNumber,
                            AccountHolderId = bankAccount.AccountHolderId,
                            AccountHolderName = accountHolderModel.Name,
                            Iban = bankAccount.BankAccountIdentifierIban,
                            Bic = bankAccount.BankAccountIdentifier.Bic,
                            Bank = bankAccount.Bank,
                            Description = bankAccount.Description
                        });
                    }
                }

                return accountHolderModel;
            }

            return null;
        }

        private readonly ITransactionService calculator;
        private readonly XFinDbContext context;

    }
}
