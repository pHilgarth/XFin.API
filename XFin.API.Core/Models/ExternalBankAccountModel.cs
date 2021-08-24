using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class ExternalBankAccountModel
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public int ExternalPartyId { get; set; }

        public string ExternalPartyName { get; set; }

        public ICollection<ExternalTransactionModel> Revenues { get; set; }
            = new List<ExternalTransactionModel>();

        public ICollection<ExternalTransactionModel> Expenses { get; set; }
            = new List<ExternalTransactionModel>();
    }
}
