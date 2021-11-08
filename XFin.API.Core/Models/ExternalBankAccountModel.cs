using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class ExternalBankAccountModel
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public int ExternalPartyId { get; set; }

        public string ExternalPartyName { get; set; }

        public List<ExternalTransactionModel> Revenues { get; set; }
            = new List<ExternalTransactionModel>();

        public List<ExternalTransactionModel> Expenses { get; set; }
            = new List<ExternalTransactionModel>();
    }
}
