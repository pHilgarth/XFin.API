using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class InternalBankAccountProfile : Profile
    {
        public InternalBankAccountProfile()
        {
            CreateMap<InternalBankAccount, InternalBankAccountModel>();
            CreateMap<InternalBankAccountUpdateModel, InternalBankAccount>().ReverseMap();
        }
    }
}
