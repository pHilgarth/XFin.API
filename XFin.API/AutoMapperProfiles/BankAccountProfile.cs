using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountModel>();
            CreateMap<BankAccountCreationModel, BankAccount>();
            CreateMap<BankAccount, BankAccountUpdateModel>().ReverseMap();
        }
    }
}
