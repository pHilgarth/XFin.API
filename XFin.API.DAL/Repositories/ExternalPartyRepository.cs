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
    public class ExternalPartyRepository : IExternalPartyRepository
    {
        public ExternalPartyRepository(IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ExternalParty CreateExternalParty(ExternalPartyCreationModel externalPartyModel)
        {
            var newExternalParty = mapper.Map<ExternalParty>(externalPartyModel);

            context.ExternalParties.Add(newExternalParty);
            context.SaveChanges();

            return newExternalParty;
        }

        public List<ExternalPartyModel> GetExternalParties()
        {
            var externalParties = context.ExternalParties.ToList();
            var externalPartyModels = new List<ExternalPartyModel>();

            foreach (var externalParty in externalParties)
            {
                var externalBankAccountId = context.ExternalBankAccounts.
                    Where(e => e.ExternalPartyId == externalParty.Id)
                    .FirstOrDefault()
                    .Id;

                var externalPartyModel = mapper.Map<ExternalPartyModel>(externalParty);
                externalPartyModel.ExternalBankAccountId = externalBankAccountId;

                externalPartyModels.Add(externalPartyModel);
            }

            return externalPartyModels != null ? externalPartyModels : null;
        }

        private readonly XFinDbContext context;
        private readonly IMapper mapper;
    }
}
