using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

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

        public BankAccountModel Create(BankAccountCreationModel bankAccount)
        {
            if (context.AccountHolders.Where(a => a.Id == bankAccount.AccountHolderId).FirstOrDefault() != null)
            {
                var newBankAccount = mapper.Map<BankAccount>(bankAccount);

                if (newBankAccount.Description == null)
                {
                    newBankAccount.Description = "Konto";
                }

                context.BankAccounts.Add(newBankAccount);
                context.SaveChanges();

                return mapper.Map<BankAccountModel>(newBankAccount);
            }

            return null;
        }

        public List<BankAccountModel> GetAllByUser(int userId)
        {
            var bankAccounts = context.BankAccounts
                .Where(b => b.UserId == userId)
                .Include(b => b.AccountHolder)
                .Include(b => b.Revenues)
                .Include(b => b.Expenses)
                .ToList();

            var bankAccountModels = new List<BankAccountModel>();

            foreach(var bankAccount in bankAccounts)
            {
                var revenues = bankAccount.Revenues
                    .Where(r => r.Executed && r.TransactionType != TransactionType.AccountTransfer && !r.IsCashTransaction)
                    .ToList();

                var cashRevenues = bankAccount.Revenues
                    .Where(r => r.Executed && r.TransactionType != TransactionType.AccountTransfer && r.IsCashTransaction)
                    .ToList();

                var expenses = bankAccount.Expenses
                    .Where(e => e.Executed && e.TransactionType != TransactionType.AccountTransfer && !e.IsCashTransaction)
                    .ToList();

                var cashExpenses = bankAccount.Expenses
                    .Where(e => e.Executed && e.TransactionType != TransactionType.AccountTransfer && e.IsCashTransaction)
                    .ToList();

                var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);

                bankAccountModel.Balance = calculator.CalculateBalance(revenues, expenses, DateTime.Now.Year, DateTime.Now.Month);
                bankAccountModel.Cash = calculator.CalculateBalance(cashRevenues, cashExpenses, DateTime.Now.Year, DateTime.Now.Month);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);

                bankAccountModels.Add(bankAccountModel);
            }

            return bankAccountModels;
        }

        public BankAccountModel GetSingle(int id, int year, int month)
        {

            var bankAccount = context.BankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)

                .Include(b => b.Revenues).ThenInclude(t => t.SourceBankAccount)
                .Include(b => b.Revenues).ThenInclude(t => t.TargetBankAccount)

                .Include(b => b.Revenues).ThenInclude(t => t.SourceCostCenter)
                .Include(b => b.Revenues).ThenInclude(t => t.TargetCostCenter)

                .Include(b => b.Expenses).ThenInclude(t => t.SourceBankAccount)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetBankAccount)

                .Include(b => b.Expenses).ThenInclude(t => t.SourceCostCenter)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetCostCenter)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                bankAccount.Revenues = calculator.GetTransactionsInMonth(
                    bankAccount.Revenues.Where(r => r.TransactionType != TransactionType.AccountTransfer && r.Executed).ToList(),
                    year,
                    month
                );

                bankAccount.Expenses = calculator.GetTransactionsInMonth(
                    bankAccount.Expenses.Where(e => e.TransactionType != TransactionType.AccountTransfer && e.Executed).ToList(),
                    year,
                    month
                );

                var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);

                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                var revenueModels = new List<TransactionBasicModel>();

                foreach (var revenue in bankAccount.Revenues)
                {
                    var revenueModel = mapper.Map<TransactionBasicModel>(revenue);

                    revenueModel.SourceAccountHolder = revenue.SourceBankAccount != null
                        ? context.AccountHolders
                            .Where(a => a.Id == revenue.SourceBankAccount.AccountHolderId)
                            .FirstOrDefault().Name
                        : null;

                    revenueModel.SourceCostCenterName = revenue.SourceCostCenter != null
                        ? revenue.SourceCostCenter.Name
                        : revenue.SourceBankAccount != null
                            ? "Nicht zugewiesen"
                            : null;

                    revenueModel.TargetCostCenterName = revenue.TargetCostCenter != null
                        ? revenue.TargetCostCenter.Name
                        : "Nicht zugewiesen";

                    revenueModels.Add(revenueModel);
                }

                var expenseModels = new List<TransactionBasicModel>();

                foreach (var expense in bankAccount.Expenses)
                {
                    var expenseModel = mapper.Map<TransactionBasicModel>(expense);

                    expenseModel.TargetAccountHolder = expense.TargetBankAccount != null
                        ? context.AccountHolders
                            .Where(a => a.Id == expense.TargetBankAccount.AccountHolderId)
                            .FirstOrDefault().Name
                        : null;

                    expenseModel.SourceCostCenterName = expense.SourceCostCenter != null
                        ? expense.SourceCostCenter.Name
                        : "Nicht zugewiesen";

                    expenseModel.TargetCostCenterName = expense.TargetCostCenter != null
                        ? expense.TargetCostCenter.Name
                        : expense.TargetBankAccount != null
                            ? "Nicht zugewiesen"
                            : null;

                    expenseModels.Add(expenseModel);
                }

                bankAccountModel.Expenses = expenseModels;
                bankAccountModel.Revenues = revenueModels;

                //foreach (var revenue in bankAccountModel.Revenues)
                //{
                //    if (revenue.SourceBankAccount != null)
                //    {
                //        revenue.SourceBankAccount.Expenses = null;
                //        revenue.SourceBankAccount.Revenues = null;
                //    }

                //    revenue.TargetBankAccount.Expenses = null;
                //    revenue.TargetBankAccount.Revenues = null;

                //}

                //var revenues = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, false);
                //var expenses = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, false);

                //bankAccountModel.Revenues = mapper.Map<List<TransactionModel>>(revenues);

                //for (int i = 0; i < revenues.Count; i++)
                //{
                //    var revenueEntity = revenues[i];
                //    var counterParty = calculator.GetAccountNumber(context.Transactions
                //        .Where(t => t.TransactionToken == revenueEntity.CounterPartTransactionToken)
                //        .Select(tt => tt.BankAccount.Iban)
                //        .FirstOrDefault());

                //    if (counterParty == null)
                //    {
                //        counterParty = context.ExternalTransactions
                //        .Where(t => t.TransactionToken == revenueEntity.CounterPartTransactionToken)
                //        .Select(t => t.ExternalBankAccount.ExternalParty.Name)
                //        .FirstOrDefault();
                //    }

                //    bankAccountModel.Revenues[i].CounterParty = counterParty != null ? counterParty : "[Kontoinitialisierung]";
                //}

                //bankAccountModel.Expenses = mapper.Map<List<TransactionModel>>(expenses);

                //for (int i = 0; i < expenses.Count; i++)
                //{
                //    var expenseEntity = expenses[i];
                //    var counterParty = calculator.GetAccountNumber(context.Transactions
                //        .Where(t => t.TransactionToken == expenseEntity.CounterPartTransactionToken)
                //        .Select(tt => tt.BankAccount.Iban)
                //        .FirstOrDefault());

                //    if (counterParty == null)
                //    {
                //        counterParty = context.ExternalTransactions
                //        .Where(t => t.TransactionToken == expenseEntity.CounterPartTransactionToken)
                //        .Select(t => t.ExternalBankAccount.ExternalParty.Name)
                //        .FirstOrDefault();
                //    }

                //    bankAccountModel.Expenses[i].CounterParty = counterParty;
                //}

                //bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, year, month);
                //bankAccountModel.ProportionPreviousMonth = calculator.GetProportionPreviousMonth(bankAccount.Transactions, year, month);

                return bankAccountModel;
            }

            return null;
        }

        public BankAccountModel GetSingleByUserAndIban(int userId, string iban)
        {
            var bankAccount = context.BankAccounts.Where(b => b.UserId == userId && b.Iban == iban).FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                return bankAccountModel;
            }

            return null;
        }

        public BankAccountModel Update(int id, JsonPatchDocument<BankAccountUpdateModel> bankAccountPatch)
        {
            var bankAccount = context.BankAccounts.Where(b => b.Id == id).FirstOrDefault();

            if (bankAccount != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var bankAccountToPatch = mapper.Map<BankAccountUpdateModel>(bankAccount);

                bankAccountPatch.ApplyTo(bankAccountToPatch);

                mapper.Map(bankAccountToPatch, bankAccount);

                context.SaveChanges();

                return mapper.Map<BankAccountModel>(bankAccount);
            }

            return null;
        }

        private readonly ITransactionService calculator;
        private readonly ITransactionRepository transactionRepo;
        private readonly IMapper mapper;
        private readonly XFinDbContext context;

        private CostCenterModel GetCostCenterModel(Transaction transaction)
        {
            //if (transaction != null)
            //{
            //    var costCenter = context.CostCenters.Where(t => t.Id == transaction.CostCenterId).FirstOrDefault();

            //    return new CostCenterModel
            //    {
            //        Id = transaction.CostCenter.Id,
            //        Name = transaction.CostCenter.Name
            //    };
            //}

            return null;
        }
    }
}