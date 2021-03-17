using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
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

        public BankAccountModel GetBankAccount(int id, bool includeTransactions, int year, int month)
        {
            var bankAccount = context.BankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.BankAccountIdentifier)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            var iban = bankAccount.BankAccountIdentifierIban;

            var bankAccountModel = new BankAccountModel
            {
                Id = bankAccount.Id,
                AccountHolderId = bankAccount.AccountHolderId,
                AccountHolderName = bankAccount.AccountHolder.Name,
                Balance = transactionService.CalculateBalance(bankAccount, year, month),
                AccountNumber = GetAccountNumberFromIban(iban),
                Iban = iban,
                Bic = bankAccount.BankAccountIdentifier.Bic,
                Bank = bankAccount.Bank,
                AccountType = bankAccount.AccountType,
                Revenues = new List<TransactionModel>(),
                Expenses = new List<TransactionModel>()
            };

            if (includeTransactions)
            {
                var revenues = transactionService.GetRevenuesInMonth(bankAccount.Transactions, year, month);
                var expenses = transactionService.GetExpensesInMonth(bankAccount.Transactions, year, month);

                foreach (var revenue in revenues)
                {
                    var counterPartTransaction = revenue.CounterPartTransactionToken == null
                        ? null : GetCounterPartTransaction(revenue);

                    bankAccountModel.Revenues.Add(new TransactionModel
                    {
                        Id = revenue.Id,
                        BankAccountId = revenue.BankAccountId,
                        CounterPartAccountNumber = GetCounterPartAccountNumber(counterPartTransaction),
                        Date = revenue.Date,
                        Amount = revenue.Amount,
                        Reference = revenue.Reference,
                        ExternalParty = GetExternalPartyModel(revenue),
                        CounterPartTransactionCategory = GetTransactionCategoryModel(counterPartTransaction),
                        TransactionCategory = GetTransactionCategoryModel(revenue)
                    });
                }

                foreach (var expense in expenses)
                {
                    var counterPartTransaction = expense.CounterPartTransactionToken == null
                        ? null : GetCounterPartTransaction(expense);

                    bankAccountModel.Expenses.Add(new TransactionModel
                    {
                        Id = expense.Id,
                        BankAccountId = expense.BankAccountId,
                        CounterPartAccountNumber = GetCounterPartAccountNumber(counterPartTransaction),
                        Date = expense.Date,
                        Amount = expense.Amount,
                        Reference = expense.Reference,
                        ExternalParty = GetExternalPartyModel(expense),
                        CounterPartTransactionCategory = GetTransactionCategoryModel(counterPartTransaction),
                        TransactionCategory = GetTransactionCategoryModel(expense)
                    });
                }
            }

            return bankAccountModel;
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
        
        private string GetAccountNumberFromIban(string iban)
        {
            return iban.Substring(iban.Length - 10).TrimStart('0');
        }

        private string GetCounterPartAccountNumber(Transaction counterPartTransaction)
        {
            return counterPartTransaction == null
                ? null : GetAccountNumberFromIban(counterPartTransaction.BankAccount.BankAccountIdentifierIban);
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
    }
}
