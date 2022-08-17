using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class RecurringTransactionProfile : Profile
    {
        public RecurringTransactionProfile()
        {
            CreateMap<RecurringTransaction, RecurringTransactionModel>();
            CreateMap<RecurringTransactionCreationModel, RecurringTransaction>();
        }
    }
}
