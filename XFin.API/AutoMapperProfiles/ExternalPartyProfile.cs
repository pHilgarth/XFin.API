using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class ExternalPartyProfile : Profile
    {
        public ExternalPartyProfile()
        {
            CreateMap<ExternalPartyCreationModel, ExternalParty>();
            CreateMap<ExternalParty, ExternalPartyModel>();
        }
    }
}
