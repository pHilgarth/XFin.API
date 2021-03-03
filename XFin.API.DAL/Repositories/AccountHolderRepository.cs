using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        /*************************************************************************************************************
         * 
         * Constructors
         * 
        *************************************************************************************************************/
        public AccountHolderRepository(XFinDbContext context)
        {
            this.context = context;
        }

        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        public List<AccountHolderModel> GetAccountHolders(bool includeAccounts)
        {
            return new List<AccountHolderModel>();
            //var accountHolders = new List<AccountHolderModel>();

            //foreach (var accountHolder in context.AccountHolders.Include(d => d.BankAccounts))
            //{
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

        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        private readonly XFinDbContext context;

    }
}
