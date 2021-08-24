using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<InternalBankAccount, InternalBankAccountModel>();
            CreateMap<InternalBankAccount, InternalBankAccountSimpleModel>();
            CreateMap<InternalBankAccountUpdateModel, InternalBankAccount>().ReverseMap();
        }
    }
}
