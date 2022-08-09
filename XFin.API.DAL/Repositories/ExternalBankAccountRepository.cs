using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class ExternalBankAccountRepository : IExternalBankAccountRepository
    {
        public ExternalBankAccountRepository(IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ExternalBankAccount CreateExternalBankAccount(ExternalBankAccountCreationModel bankAccount)
        {//TODO add error handling when wrong id is passed, if no accountholder with this bankAccount.id exists
            var newBankAccount = mapper.Map<ExternalBankAccount>(bankAccount);
            //var newBankAccount = new ExternalBankAccount
            //{
            //    Iban    = bankAccount.Iban != null ? bankAccount.Iban : null,
            //    Bic     = bankAccount.Bic != null ? bankAccount.Bic : null,
            //    ExternalPartyId = bankAccount.ExternalPartyId
            //};

            context.ExternalBankAccounts.Add(newBankAccount);
            context.SaveChanges();

            return newBankAccount;
        }

        private readonly IMapper mapper;
        private readonly XFinDbContext context;
    }
}
