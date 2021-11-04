using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ExternalBankAccountProfile : Profile
    {
        public ExternalBankAccountProfile()
        {
            CreateMap<ExternalBankAccount, ExternalBankAccountModel>();
        }
    }
}
