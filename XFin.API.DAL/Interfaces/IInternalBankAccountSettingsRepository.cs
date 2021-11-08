using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IInternalBankAccountSettingsRepository
    {
        InternalBankAccountSettingsModel GetInternalBankAccountSettings(int id);
    }
}
