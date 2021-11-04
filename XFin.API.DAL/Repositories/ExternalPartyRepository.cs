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

            if (externalParties != null && externalParties.Count > 0)
            {
                var externalPartyModels = new List<ExternalPartyModel>();

                foreach (var externalParty in externalParties)
                {
                    var externalBankAccount = context.ExternalBankAccounts.
                        Where(e => e.ExternalPartyId == externalParty.Id)
                        .FirstOrDefault();

                    var externalBankAccountModel = mapper.Map<ExternalBankAccountModel>(externalBankAccount);

                    var externalPartyModel = mapper.Map<ExternalPartyModel>(externalParty);
                    externalPartyModel.ExternalBankAccount = externalBankAccountModel;

                    externalPartyModels.Add(externalPartyModel);
                }

                return externalPartyModels != null && externalPartyModels.Count > 0 ? externalPartyModels : null;
            }

            return null;
        }

        private readonly XFinDbContext context;
        private readonly IMapper mapper;
    }
}
