using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<LoanModel> GetAll()
        {
            return mapper.Map<List<LoanModel>>(context.Loans);
        }

        public List<LoanModel> GetAllByAccount(int bankAccountId)
        {
            var loans = context.Loans
                .Where(l => l.InternalBankAccount.Id == bankAccountId)
                .ToList();

            return mapper.Map<List<LoanModel>>(loans);
        }
    }
}