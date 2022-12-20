using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class CostCenterAssetProfile : Profile
    {
        public CostCenterAssetProfile()
        {
            CreateMap<CostCenterAsset, CostCenterAssetModel>();
            CreateMap<CostCenterAssetCreationModel, CostCenterAsset>();
            CreateMap<CostCenterAssetUpdateModel, CostCenterAsset>().ReverseMap();
        }
    }
}
