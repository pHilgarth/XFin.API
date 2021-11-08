using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IExternalBankAccountRepository
    {
        ExternalBankAccount CreateExternalBankAccount(ExternalBankAccountCreationModel bankAccount);
    }
}
