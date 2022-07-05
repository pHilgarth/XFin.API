using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ReserveProfile : Profile
    {
        public ReserveProfile()
        {
            //CreateMap<AccountHolder, AccountHolderModel>();
            //CreateMap<AccountHolder, AccountHolderSimpleModel>();
            CreateMap<ReserveCreationModel, Reserve>();
            //CreateMap<AccountHolderUpdateModel, AccountHolder>().ReverseMap();
        }
    }
}
