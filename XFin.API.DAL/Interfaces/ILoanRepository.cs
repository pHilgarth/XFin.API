using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface ILoanRepository
    {
        LoanModel Create(LoanCreationModel loan);
        List<LoanModel> GetAllByBankAccount(int accountId);
        List<LoanModel> GetAllByCreditorAndDebitor(int firstAccountId, int secondAccountId);
        LoanModel GetSingleById(int loanId);
        LoanModel Update(int id, JsonPatchDocument<LoanUpdateModel> costCenterAssetPatch);
    }

}
