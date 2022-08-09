using System.Collections.Generic;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ILoanRepository
    {
        LoanModel Create(LoanCreationModel loan);
        List<LoanModel> GetAll();
        List<LoanModel> GetAllByAccount(int bankAccountId);
    }
}
