using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class AccountHolderProfile : Profile
    {
        public AccountHolderProfile()
        {
            CreateMap<AccountHolder, AccountHolderModel>();
            CreateMap<AccountHolder, AccountHolderSimpleModel>();
            CreateMap<AccountHolderCreationModel, AccountHolder>();
            CreateMap<AccountHolderUpdateModel, AccountHolder>().ReverseMap();
        }
    }
}
