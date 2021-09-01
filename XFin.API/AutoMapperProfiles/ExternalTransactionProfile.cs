using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ExternalTransactionProfile : Profile
    {
        public ExternalTransactionProfile()
        {
            CreateMap<ExternalTransaction, ExternalTransactionModel>();
            CreateMap<ExternalTransactionCreationModel, ExternalTransaction>();
        }
    }
}
