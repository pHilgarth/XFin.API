using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class InternalBankAccountSettingsProfile : Profile
    {
        public InternalBankAccountSettingsProfile()
        {
            CreateMap<InternalBankAccountSettings, InternalBankAccountSettingsModel>();
        }
    }
}
