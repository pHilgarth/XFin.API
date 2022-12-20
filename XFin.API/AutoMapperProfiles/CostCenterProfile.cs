using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class CostCenterProfile : Profile
    {
        public CostCenterProfile()
        {
            CreateMap<CostCenter, CostCenterModel>();
            CreateMap<CostCenter, CostCenterSimpleModel>();
            CreateMap<CostCenterCreationModel, CostCenter>();
            CreateMap<CostCenterUpdateModel, CostCenter>().ReverseMap();
        }
    }
}
