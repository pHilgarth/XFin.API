using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class AccountHolderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IInternalBankAccountModel> BankAccounts { get; set; }
            = new List<IInternalBankAccountModel>();
    }
}
