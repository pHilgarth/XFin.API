using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionBasicModel Create(TransactionCreationModel transaction);
    }
}
