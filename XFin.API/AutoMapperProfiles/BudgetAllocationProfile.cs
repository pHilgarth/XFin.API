using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class BudgetAllocationProfile : Profile
    {
        public BudgetAllocationProfile()
        {
            CreateMap<BudgetAllocation, BudgetAllocationBasicModel>();
            CreateMap<BudgetAllocationCreationModel, BudgetAllocation>();
        }
    }
}
