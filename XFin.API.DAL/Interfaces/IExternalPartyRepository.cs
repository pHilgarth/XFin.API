using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IExternalPartyRepository
    {
        ExternalParty CreateExternalParty(ExternalPartyCreationModel externalParty);
        List<ExternalPartyModel> GetExternalParties();
    }
}
