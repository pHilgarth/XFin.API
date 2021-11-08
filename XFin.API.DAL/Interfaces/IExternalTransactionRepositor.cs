using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IExternalTransactionRepository
    {
        ExternalTransaction CreateExternalTransaction(ExternalTransactionCreationModel transaction);
    }
}
