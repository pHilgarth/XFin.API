using System.Collections.Generic;
using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionCreationModel, Transaction>();
            CreateMap<Transaction, TransactionModel>();
        }
    }
}
