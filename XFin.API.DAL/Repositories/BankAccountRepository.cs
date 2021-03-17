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
                AccountHolder = bankAccount.AccountHolder.Name,
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
                    var externalParty = context.ExternalParties.Where(e => e.Id == revenue.ExternalPartyId)
                        .Include(e => e.BankAccountIdentifier)
                        .FirstOrDefault();

                    var transactionCategory = context.TransactionCategories.Where(t => t.Id == revenue.TransactionCategoryId).FirstOrDefault();

                    bankAccountModel.Revenues.Add(new TransactionModel
                    {
                        Id = revenue.Id,
                        BankAccountId = revenue.BankAccountId,
                        SourceAccountNumber = GetTransactionSourceAccountNumber(revenue.CounterPartTransactionToken),
                        Date = revenue.Date,
                        Amount = revenue.Amount,
                        Reference = revenue.Reference,
                        ExternalParty = externalParty == null ? null : new ExternalPartyModel
                        {
                            Id = externalParty.Id,
                            Iban = externalParty.BankAccountIdentifierIban,
                            Bic = externalParty.BankAccountIdentifier.Bic,
                            Name = externalParty.Name
                        },
                        TransactionCategory = transactionCategory == null ? null : new TransactionCategoryModel
                        {
                            Id = transactionCategory.Id,
                            Name = transactionCategory.Name
                        }
                    });
                }

                foreach (var expense in expenses)
                {
                    bankAccountModel.Expenses.Add(new TransactionModel
                    {
                        Id = expense.Id,
                        BankAccountId = expense.BankAccountId,
                        SourceAccountNumber = GetTransactionSourceAccountNumber(expense.CounterPartTransactionToken),
                        Date = expense.Date,
                        Amount = expense.Amount,
                        Reference = expense.Reference,
                        ExternalParty = new ExternalPartyModel
                        {
                            Id = expense.ExternalParty.Id,
                            Iban = expense.ExternalParty.BankAccountIdentifierIban,
                            Bic = expense.ExternalParty.BankAccountIdentifier.Bic,
                            Name = expense.ExternalParty.Name
                        },
                        TransactionCategory = new TransactionCategoryModel
                        {
                            Id = expense.TransactionCategory.Id,
                            Name = expense.TransactionCategory.Name
                        }
                    });
                }
            }

            return bankAccountModel;
        }

        private readonly ITransactionService transactionService;
        private readonly XFinDbContext context;

        private string GetAccountNumberFromIban(string iban)
        {
            return iban.Substring(iban.Length - 10).TrimStart('0');
        }

        private string GetTransactionSourceAccountNumber(string transactionToken)
        {
            var sourceIban = GetCounterPartTransaction(transactionToken).BankAccount.BankAccountIdentifierIban;

            return GetAccountNumberFromIban(sourceIban);
        }

        private Transaction GetCounterPartTransaction(string transactionToken)
        {
            return context.Transactions.Where(t => t.CounterPartTransactionToken == transactionToken).FirstOrDefault();
        }
    }
}
