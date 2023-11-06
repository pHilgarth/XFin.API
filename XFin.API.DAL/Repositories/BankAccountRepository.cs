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
        public BankAccountRepository(ICalculatorService calculator, ITransactionRepository transactionRepo, IMapper mapper, XFinDbContext context)
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
                    .Where(r => r.Executed && !r.IsCashTransaction)
                    .ToList();

                var cashRevenues = bankAccount.Revenues
                    .Where(r => r.Executed && r.IsCashTransaction)
                    .ToList();

                var expenses = bankAccount.Expenses
                    .Where(e => e.Executed && !e.IsCashTransaction)
                    .ToList();

                var cashExpenses = bankAccount.Expenses
                    .Where(e => e.Executed && e.IsCashTransaction)
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
                .Include(b => b.Revenues).ThenInclude(t => t.CostCenter)
                .Include(b => b.Revenues).ThenInclude(t => t.CostCenterAsset)

                .Include(b => b.Expenses).ThenInclude(t => t.SourceBankAccount)
                .Include(b => b.Expenses).ThenInclude(t => t.TargetBankAccount)
                .Include(b => b.Expenses).ThenInclude(t => t.CostCenter)
                .Include(b => b.Expenses).ThenInclude(t => t.CostCenterAsset)

                .FirstOrDefault();

            if (bankAccount != null)
            {
                var executedRevenues = bankAccount.Revenues.Where(r => r.Executed).ToList();
                var executedExpenses = bankAccount.Expenses.Where(e => e.Executed).ToList();

                bankAccount.Revenues = calculator.GetTransactionsInMonth(
                    executedRevenues, year, month)
                    .OrderBy(r => r.Date)
                    .ToList();

                bankAccount.Expenses = calculator.GetTransactionsInMonth(
                    executedExpenses, year, month)
                    .OrderBy(e => e.Date)
                    .ToList();

                var bankAccountModel = mapper.Map<BankAccountModel>(bankAccount);

                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);
                bankAccountModel.Balance = calculator.CalculateBalance(executedRevenues, executedExpenses, year, month);
                bankAccountModel.BalancePreviousMonth = calculator.GetBalancePreviousMonth(executedRevenues, executedExpenses, year, month);

                var revenueModels = new List<TransactionBasicModel>();

                foreach (var revenue in bankAccount.Revenues)
                {
                    var revenueModel = mapper.Map<TransactionBasicModel>(revenue);

                    revenueModel.SourceAccountHolder = revenue.SourceBankAccount != null
                        ? context.AccountHolders
                            .Where(a => a.Id == revenue.SourceBankAccount.AccountHolderId)
                            .FirstOrDefault().Name
                        : null;

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

                    expenseModel.CostCenterName = expense.CostCenter != null
                        ? expense.CostCenter.Name
                        : null;

                    expenseModel.CostCenterAssetName = expense.CostCenterAsset != null
                        ? expense.CostCenterAsset.Name
                        : null;

                    expenseModels.Add(expenseModel);
                }

                bankAccountModel.Expenses = expenseModels;
                bankAccountModel.Revenues = revenueModels;

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

        private readonly ICalculatorService calculator;
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