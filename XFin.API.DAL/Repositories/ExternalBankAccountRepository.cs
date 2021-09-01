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
    public class ExternalBankAccountRepository : IExternalBankAccountRepository
    {
        public ExternalBankAccountRepository(IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ExternalBankAccount CreateExternalBankAccount(ExternalBankAccountCreationModel bankAccount)
        {//TODO add error handling when wrong id is passed, if no accountholder with this bankAccount.id exists
            var newBankAccount = new ExternalBankAccount
            {
                Iban    = bankAccount.Iban != null ? bankAccount.Iban : null,
                Bic     = bankAccount.Bic != null ? bankAccount.Bic : null,
                ExternalPartyId = bankAccount.ExternalPartyId
            };

            context.ExternalBankAccounts.Add(newBankAccount);
            context.SaveChanges();

            return newBankAccount;
        }

        private readonly IMapper mapper;
        private readonly XFinDbContext context;
    }
}
