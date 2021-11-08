using System;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface IInternalBankAccountSettingsRepository
    {
        InternalBankAccountSettingsModel GetInternalBankAccountSettings(int id);
    }
}
