using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class TransactionCategoryProfile : Profile
    {
        public TransactionCategoryProfile()
        {
            CreateMap<TransactionCategory, TransactionCategoryModel>();
            CreateMap<TransactionCategory, TransactionCategorySimpleModel>();
        }
    }
}
