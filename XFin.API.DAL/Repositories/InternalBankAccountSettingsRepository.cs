using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class InternalBankAccountSettingsRepository : IInternalBankAccountSettingsRepository
    {
        public InternalBankAccountSettingsRepository(XFinDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public InternalBankAccountSettingsModel GetInternalBankAccountSettings(int id)
        {
            var settings = context.InternalBankAccountSettings.Where(i => i.InternalBankAccountId == id).FirstOrDefault();

            return settings != null
                ? mapper.Map<InternalBankAccountSettingsModel>(settings)
                : null;
        }


        public InternalBankAccountSettings Update(int settingsId, JsonPatchDocument<InternalBankAccountSettingsUpdateModel> settingsPatch)
        {
            var settingsEntity = context.InternalBankAccountSettings.Where(b => b.Id == settingsId).FirstOrDefault();

            if (settingsEntity != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var settingsToPatch = mapper.Map<InternalBankAccountSettingsUpdateModel>(settingsEntity);

                settingsPatch.ApplyTo(settingsToPatch);

                mapper.Map(settingsToPatch, settingsEntity);

                context.SaveChanges();

                return settingsEntity;
            }

            return null;
        }

        private readonly XFinDbContext context;
        private readonly IMapper mapper;
    }
}
