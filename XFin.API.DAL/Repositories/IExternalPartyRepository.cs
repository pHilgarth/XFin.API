using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IExternalPartyRepository
    {
        ExternalParty CreateExternalParty(ExternalPartyCreationModel externalParty);
        List<ExternalPartyModel> GetExternalParties();
    }
}
