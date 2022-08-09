using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ExternalBankAccountProfile : Profile
    {
        public ExternalBankAccountProfile()
        {
            CreateMap<ExternalBankAccount, ExternalBankAccountModel>();
            CreateMap<ExternalBankAccountCreationModel, ExternalBankAccount>();
        }
    }
}
