using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        public BankAccountRepository(ITransactionService calculator, ITransactionRepository transactionRepo, IMapper mapper, XFinDbContext context)
        {
            this.calculator = calculator;
            this.transactionRepo = transactionRepo;
            this.context = context;
            this.mapper = mapper;
        }

        public InternalBankAccount CreateBankAccount(InternalBankAccountCreationModel bankAccount)
        {//TODO add error handling when wrong id is passed, if no accountholder with this bankAccount.id exists
            var accountHolder = context.AccountHolders.Where(a => a.Id == bankAccount.AccountHolderId).FirstOrDefault();

            if (accountHolder == null)
            {
                return null;
            }

            var newBankAccount = new InternalBankAccount
            {
                Iban                = bankAccount.Iban,
                Bic                 = bankAccount.Bic,
                AccountHolderId     = bankAccount.AccountHolderId,
                Bank                = bankAccount.Bank,
                Description         = bankAccount.Description
            };

            context.InternalBankAccounts.Add(newBankAccount);
            context.SaveChanges();

            if (bankAccount.Balance != 0)
            {
                var initializationTransaction = new InternalTransactionCreationModel
                {
                    InternalBankAccountId = newBankAccount.Id,
                    DateString = DateTime.Now.ToShortDateString(),
                    Amount = bankAccount.Balance,
                    Reference = "[Kontoinitialisierung]",
                };

                transactionRepo.CreateTransaction(initializationTransaction);
            }

            return newBankAccount;
        }

        public InternalBankAccountModel GetBankAccount(int id, int year, int month)
        {
            var bankAccount = context.InternalBankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.Transactions).ThenInclude(t => t.TransactionCategory)
                .Include(b => b.Transactions).ThenInclude(t => t.InternalBankAccount)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<InternalBankAccountModel>(bankAccount);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                var revenues = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month);
                var expenses = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month);

                bankAccountModel.Revenues = mapper.Map<List<InternalTransactionModel>>(revenues);

                for (int i = 0; i < revenues.Count; i++)
                {
                    var revenueEntity = revenues[i];
                    var counterParty = calculator.GetAccountNumber(context.InternalTransactions
                        .Where(t => t.TransactionToken == revenueEntity.CounterPartTransactionToken)
                        .Select(tt => tt.InternalBankAccount.Iban)
                        .FirstOrDefault());

                    if (counterParty == null)
                    {
                        counterParty = context.ExternalTransactions
                        .Where(t => t.TransactionToken == revenueEntity.CounterPartTransactionToken)
                        .Select(t => t.ExternalBankAccount.ExternalParty.Name)
                        .FirstOrDefault();
                    }

                    bankAccountModel.Revenues[i].CounterParty = counterParty != null ? counterParty : "[Kontoinitialisierung]";
                }

                bankAccountModel.Expenses = mapper.Map<List<InternalTransactionModel>>(expenses);

                for (int i = 0; i < expenses.Count; i++)
                {
                    var expenseEntity = expenses[i];
                    var counterParty = calculator.GetAccountNumber(context.InternalTransactions
                        .Where(t => t.TransactionToken == expenseEntity.CounterPartTransactionToken)
                        .Select(tt => tt.InternalBankAccount.Iban)
                        .FirstOrDefault());

                    if (counterParty == null)
                    {
                        counterParty = context.ExternalTransactions
                        .Where(t => t.TransactionToken == expenseEntity.CounterPartTransactionToken)
                        .Select(t => t.ExternalBankAccount.ExternalParty.Name)
                        .FirstOrDefault();
                    }

                    bankAccountModel.Expenses[i].CounterParty = counterParty;
                }

                bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, year, month);
                bankAccountModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(bankAccount.Transactions, year, month);

                return bankAccountModel;
            }

            return null;
        }

        public InternalBankAccountSimpleModel GetBankAccountSimple(int id, int year, int month)
        {
            var bankAccount = context.InternalBankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<InternalBankAccountSimpleModel>(bankAccount);
                bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, year, month);

                return bankAccountModel;
            }

            return null;
        }

        public InternalBankAccount UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch)
        {
            var bankAccountEntity = context.InternalBankAccounts.Where(b => b.Id == id).FirstOrDefault();
            var bankAccountToPatch = mapper.Map<InternalBankAccountUpdateModel>(bankAccountEntity);

            bankAccountPatch.ApplyTo(bankAccountToPatch);

            mapper.Map(bankAccountToPatch, bankAccountEntity);

            context.SaveChanges();

            return bankAccountEntity;
        }

        private readonly ITransactionService calculator;
        private readonly ITransactionRepository transactionRepo;
        private readonly IMapper mapper;
        private readonly XFinDbContext context;

        //private InternalTransaction GetCounterPartTransaction(InternalTransaction transaction)
        //{
        //    var counterPartTransaction = context.InternalTransactions
        //        .Where(t => t.CounterPartTransactionToken == transaction.CounterPartTransactionToken && t.Id != transaction.Id)
        //        .Include(t => t.InternalBankAccount)
        //        .FirstOrDefault();

        //    if (counterPartTransaction == null)
        //    {
        //        counterPartTransaction = context.ExternalTransactions
        //            .Where(t => t.CounterPartTransactionToken == transaction.CounterPartTransactionToken && t.Id != transaction.Id)
        //            .Include(t => t.ExternalBankAccount)
        //            .FirstOrDefault();
        //    }
        //}

        private TransactionCategoryModel GetTransactionCategoryModel(InternalTransaction transaction)
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

        //private string GetTransactionType(InternalTransaction transaction)
        //{
        //    if (transaction.ExternalPartyId == null && transaction.CounterPartTransactionToken == null)
        //    {
        //        return Enum.GetName(typeof(TransactionType), TransactionType.Initialization);
        //    }
        //    else if (transaction.ExternalPartyId == null)
        //    {
        //        return Enum.GetName(typeof(TransactionType), TransactionType.Transfer);
        //    }
        //    else
        //    {
        //        return transaction.Amount > 0
        //            ? Enum.GetName(typeof(TransactionType), TransactionType.Revenue)
        //            : Enum.GetName(typeof(TransactionType), TransactionType.Expense);
        //    }
        //}
    }
}
