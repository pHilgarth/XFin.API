using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IInternalBankAccountSettingsRepository
    {
        InternalBankAccountSettingsModel GetInternalBankAccountSettings(int id);
        InternalBankAccountSettings Update(int settingsId, JsonPatchDocument<InternalBankAccountSettingsUpdateModel> settingsPatch);
    }
}
