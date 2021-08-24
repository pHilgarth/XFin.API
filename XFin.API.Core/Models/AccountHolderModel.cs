using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class AccountHolderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<IInternalBankAccountModel> BankAccounts { get; set; }
            = new List<IInternalBankAccountModel>();
    }
}
