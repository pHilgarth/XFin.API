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
    public class InternalBankAccountRepository : IInternalBankAccountRepository
    {
        public InternalBankAccountRepository(ITransactionService calculator, IInternalTransactionRepository transactionRepo, IMapper mapper, XFinDbContext context)
        {
            this.calculator = calculator;
            this.transactionRepo = transactionRepo;
            this.context = context;
            this.mapper = mapper;
        }

        public InternalBankAccount CreateBankAccount(InternalBankAccountCreationModel bankAccount)
        {
            //check if "bankAccount" already exists
            if (context.InternalBankAccounts.Where(b => b.Iban == bankAccount.Iban).FirstOrDefault() != null)
            {
                return null;
            }

            //TODO - can I delete this?
            //var accountholder = context.accountholders.where(a => a.id == bankaccount.accountholderid).firstordefault();

            //if (accountholder == null)
            //{
            //    return null;
            //}

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

            return newBankAccount;
        }

        public List<InternalBankAccountModel> GetAll()
        {
            var bankAccounts = mapper.Map<List<InternalBankAccountModel>>(
                context.InternalBankAccounts
                .Include(i => i.AccountHolder)
                .ToList());

            foreach(var bankAccount in bankAccounts)
            {
                bankAccount.AccountNumber = calculator.GetAccountNumber(bankAccount.Iban);
            }

            return bankAccounts;
        }

        public InternalBankAccountModel GetBankAccount(int id, int year, int month)
        {
            var bankAccount = context.InternalBankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.Transactions).ThenInclude(t => t.CostCenter)
                .Include(b => b.Transactions).ThenInclude(t => t.InternalBankAccount)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<InternalBankAccountModel>(bankAccount);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                var revenues = calculator.GetRevenuesInMonth(bankAccount.Transactions, year, month, false);
                var expenses = calculator.GetExpensesInMonth(bankAccount.Transactions, year, month, false);

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

        public InternalBankAccountModel GetByIban(string iban)
        {
            var bankAccount = context.InternalBankAccounts.Where(b => b.Iban == iban).FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<InternalBankAccountModel>(bankAccount);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                return bankAccountModel;
            }

            return null;
        }

        public InternalBankAccountModel GetBankAccountSimple(int id, int year, int month)
        {
            var bankAccount = context.InternalBankAccounts.Where(b => b.Id == id)
                .Include(b => b.AccountHolder)
                .Include(b => b.Transactions)
                .FirstOrDefault();

            if (bankAccount != null)
            {
                var bankAccountModel = mapper.Map<InternalBankAccountModel>(bankAccount);
                bankAccountModel.Balance = calculator.CalculateBalance(bankAccount.Transactions, year, month);
                bankAccountModel.AccountNumber = calculator.GetAccountNumber(bankAccountModel.Iban);

                return bankAccountModel;
            }

            return null;
        }

        public InternalBankAccount UpdateBankAccountPartially(int id, JsonPatchDocument<InternalBankAccountUpdateModel> bankAccountPatch)
        {
            var bankAccountEntity = context.InternalBankAccounts.Where(b => b.Id == id).FirstOrDefault();

            if (bankAccountEntity != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var bankAccountToPatch = mapper.Map<InternalBankAccountUpdateModel>(bankAccountEntity);

                bankAccountPatch.ApplyTo(bankAccountToPatch);

                mapper.Map(bankAccountToPatch, bankAccountEntity);

                context.SaveChanges();

                return bankAccountEntity;
            }

            return null;
        }

        private readonly ITransactionService calculator;
        private readonly IInternalTransactionRepository transactionRepo;
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

        private CostCenterModel GetCostCenterModel(InternalTransaction transaction)
        {
            if (transaction != null)
            {
                var costCenter = context.CostCenters.Where(t => t.Id == transaction.CostCenterId).FirstOrDefault();

                return new CostCenterModel
                {
                    Id = transaction.CostCenter.Id,
                    Name = transaction.CostCenter.Name
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