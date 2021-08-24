using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class InternalTransactionProfile : Profile
    {
        public InternalTransactionProfile()
        {
            CreateMap<InternalTransaction, InternalTransactionModel>();
        }
    }
}
