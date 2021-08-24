using System;
using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Repositories
{
    public interface ITransactionRepository
    {
        InternalTransaction CreateTransaction(InternalTransactionCreationModel transaction);
    }
}
