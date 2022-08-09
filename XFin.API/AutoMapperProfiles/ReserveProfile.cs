using System.Collections.Generic;
using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ReserveProfile : Profile
    {
        public ReserveProfile()
        {
            CreateMap<ReserveCreationModel, Reserve>();
            CreateMap<Reserve, ReserveModel>();
            //CreateMap<AccountHolderUpdateModel, AccountHolder>().ReverseMap();
        }
    }
}
