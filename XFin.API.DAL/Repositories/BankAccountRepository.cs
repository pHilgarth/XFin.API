using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        public BankAccountRepository(ITransactionService transactionService, XFinDbContext context)
        {
            this.transactionService = transactionService;
            this.context = context;
        }

        public BankAccountModel GetBankAccount(string accountNumber, bool includeTransactions, int year, int month)
        {
            var bankAccount = context.BankAccounts.Where(b => b.AccountNumber == accountNumber)
                .Include(b => b.AccountHolder)
                .Include(b => b.BankAccountIdentifier)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            if (bankAccount != null)
            {

                var bankAccountModel = new BankAccountModel
                {
                    AccountNumber           = bankAccount.AccountNumber,
                    AccountHolderId         = bankAccount.AccountHolderId,
                    AccountHolderName       = bankAccount.AccountHolder.Name,
                    Balance                 = transactionService.CalculateBalance(bankAccount, year, month),
                    ProportionPreviousMonth = transactionService.GetProportionPreviousMonth(bankAccount, year, month),
                    Iban                    = bankAccount.BankAccountIdentifierIban,
                    Bic                     = bankAccount.BankAccountIdentifier.Bic,
                    Bank                    = bankAccount.Bank,
                    AccountType             = bankAccount.AccountType,
                    Revenues                = new List<TransactionModel>(),
                    Expenses                = new List<TransactionModel>()
                };

                if (includeTransactions)
                {
                    var revenues = transactionService.GetRevenuesInMonth(bankAccount.Transactions, year, month);
                    var expenses = transactionService.GetExpensesInMonth(bankAccount.Transactions, year, month);

                    foreach (var revenue in revenues)
                    {
                        revenue.Date = DateTime.SpecifyKind(revenue.Date, DateTimeKind.Utc);

                        var counterPartTransaction = revenue.CounterPartTransactionToken == null
                            ? null : GetCounterPartTransaction(revenue);

                        bankAccountModel.Revenues.Add(new TransactionModel
                        {
                            Id                              = revenue.Id,
                            BankAccountNumber               = revenue.BankAccountAccountNumber,
                            CounterPartAccountNumber        = counterPartTransaction?.BankAccount.AccountNumber,
                            Date                            = revenue.Date,
                            Amount                          = revenue.Amount,
                            Reference                       = revenue.Reference,
                            ExternalParty                   = GetExternalPartyModel(revenue),
                            CounterPartTransactionCategory  = GetTransactionCategoryModel(counterPartTransaction),
                            TransactionCategory             = GetTransactionCategoryModel(revenue),
                            TransactionType                 = GetTransactionType(revenue)
                        });
                    }

                    foreach (var expense in expenses)
                    {
                        expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);

                        var counterPartTransaction = expense.CounterPartTransactionToken == null
                            ? null : GetCounterPartTransaction(expense);

                        bankAccountModel.Expenses.Add(new TransactionModel
                        {
                            Id                              = expense.Id,
                            BankAccountNumber               = expense.BankAccountAccountNumber,
                            CounterPartAccountNumber        = counterPartTransaction?.BankAccount.AccountNumber,
                            Date                            = expense.Date,
                            Amount                          = expense.Amount,
                            Reference                       = expense.Reference,
                            ExternalParty                   = GetExternalPartyModel(expense),
                            CounterPartTransactionCategory  = GetTransactionCategoryModel(counterPartTransaction),
                            TransactionCategory             = GetTransactionCategoryModel(expense)
                        });
                    }
                }

                return bankAccountModel;
            }

            return null;
        }

        private readonly ITransactionService transactionService;
        private readonly XFinDbContext context;

        private ExternalPartyModel GetExternalPartyModel(Transaction transaction)
        {
            var externalParty = context.ExternalParties
                .Where(e => e.Id == transaction.ExternalPartyId)
                .Include(e => e.BankAccountIdentifier)
                .FirstOrDefault();

            return externalParty == null ? null : new ExternalPartyModel
            {
                Id = externalParty.Id,
                Iban = externalParty.BankAccountIdentifierIban,
                Bic = externalParty.BankAccountIdentifier.Bic,
                Name = externalParty.Name
            };
        }

        private Transaction GetCounterPartTransaction(Transaction transaction)
        {
            return context.Transactions
                .Where(t => t.CounterPartTransactionToken == transaction.CounterPartTransactionToken && t.Id != transaction.Id)
                .Include(t => t.BankAccount)
                .FirstOrDefault();
        }

        private TransactionCategoryModel GetTransactionCategoryModel(Transaction transaction)
        {
            if (transaction != null)
            {
                var transactionCategory = context.TransactionCategories.Where(t => t.Id == transaction.TransactionCategoryId).FirstOrDefault();

                return new TransactionCategoryModel
                {
                    Id = transaction.TransactionCategory.Id,
                    Name = transaction.TransactionCategory.Name
                };
            }

            return null;
        }

        private string GetTransactionType(Transaction transaction)
        {
            if (transaction.ExternalPartyId == null && transaction.CounterPartTransactionToken == null)
            {
                return Enum.GetName(typeof(TransactionType), TransactionType.Initialization);
            }
            else if (transaction.ExternalPartyId == null)
            {
                return Enum.GetName(typeof(TransactionType), TransactionType.Transfer);
            }
            else
            {
                return transaction.Amount > 0
                    ? Enum.GetName(typeof(TransactionType), TransactionType.Revenue)
                    : Enum.GetName(typeof(TransactionType), TransactionType.Expense);
            }
        }
    }
}
