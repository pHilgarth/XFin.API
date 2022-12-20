using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        public LoanRepository(IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private readonly IMapper mapper;
        private readonly XFinDbContext context;

        public LoanModel Create(LoanCreationModel loan)
        {
            var newLoan = mapper.Map<Loan>(loan);

            context.Loans.Add(newLoan);
            context.SaveChanges();

            return mapper.Map<LoanModel>(newLoan);
        }

        public List<LoanModel> GetAllByCreditorAndDebitor(int firstAccountId, int secondAccountId)
        {
            return mapper.Map<List<LoanModel>>(
                context.Loans
                    .Where(l =>
                        l.CreditorBankAccountId == firstAccountId && l.DebitorBankAccountId == secondAccountId ||
                        l.CreditorBankAccountId == secondAccountId && l.DebitorBankAccountId == firstAccountId)
                    .Include(l => l.CreditorBankAccount)
                    .Include(l => l.DebitorBankAccount)
                    .ToList());
        }

        public List<LoanModel> GetAllByBankAccount(int accountId)
        {
            //var loans = context.Loans
            //    .Where(l => l.DebitorBankAccountId == accountId)
            return mapper.Map<List<LoanModel>>(
                context.Loans
                    .Where(l => l.DebitorBankAccountId == accountId || l.CreditorBankAccountId == accountId)
                    .Include(l => l.CreditorBankAccount)
                    .Include(l => l.DebitorBankAccount)
                    .Include(l => l.Transactions)
                    .ToList());
        }

        public LoanModel GetSingleById(int loanId)
        {
            var loan = mapper.Map<LoanModel>(
                context.Loans
                    .Where(l => l.Id == loanId)
                    .Include(l => l.CreditorBankAccount)
                    .Include(l => l.DebitorBankAccount)
                    .Include(l => l.Transactions)
                    .FirstOrDefault());

            loan.CreditorBankAccount.AccountHolderName = context.AccountHolders
                .Where(a => a.Id == loan.CreditorBankAccount.AccountHolderId)
                .FirstOrDefault()
                .Name;

            loan.DebitorBankAccount.AccountHolderName = context.AccountHolders
                .Where(a => a.Id == loan.DebitorBankAccount.AccountHolderId)
                .FirstOrDefault()
                .Name;

            return loan;
        }

        public LoanModel Update(int id, JsonPatchDocument<LoanUpdateModel> loanPatch)
        {
            var loan = context.Loans.Where(l => l.Id == id).FirstOrDefault();

            if (loan != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var loanToPatch = mapper.Map<LoanUpdateModel>(loan);

                loanPatch.ApplyTo(loanToPatch);

                mapper.Map(loanToPatch, loan);

                context.SaveChanges();

                return mapper.Map<LoanModel>(loan);
            }

            return null;
        }
    }
}