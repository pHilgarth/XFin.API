using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class AccountHolderModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public bool External { get; set; }

        public List<BankAccountModel> BankAccounts { get; set; }
    }
}
