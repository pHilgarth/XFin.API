using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
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

        public List<BankAccountModel> GetAll()
        {
            var bankAccounts = mapper.Map<List<BankAccountModel>>(
                context.BankAccounts
                .Include(i => i.AccountHolder)
                .ToList());

            foreach(var bankAccount in bankAccounts)
            {
                bankAccount.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);
            }

            return bankAccounts;
        }

        public BankAccountModel GetSingle(int id, int year, int month)
        {
            var bankAccount = context.BankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.Revenues).ThenInclude(t => t.SourceBankAccount)
                .Include(b => b.Revenues).ThenInclude(t => t.TargetBankAccount)
                .Include(b => b.Expenses).ThenInclude(t => t.SourceBankAccount)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetBankAccount)
                .Include(b => b.Revenues).ThenInclude(t => t.SourceCostCenter)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetCostCenter)
                .Include(b => b.Revenues).ThenInclude(t => t.SourceCostCenter)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetCostCenter)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                //foreach (var expense in bankAccountModel.Expenses)
                //{
                //    expense.SourceBankAccount.Expenses = null;
                //    expense.SourceBankAccount.Revenues = null;

                //    expense.TargetBankAccount.Expenses = null;
                //    expense.TargetBankAccount.Revenues = null;
                //}

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

        public BankAccountModel GetByIban(string iban)
        {
            var bankAccount = context.BankAccounts.Where(b => b.Iban == iban).FirstOrDefault();

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