using AutoMapper;
using System.Linq;
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

        private readonly XFinDbContext context;
        private readonly IMapper mapper;
    }
}
