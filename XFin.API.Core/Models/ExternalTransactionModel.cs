using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class ExternalTransactionModel
    {
        public int ExternalBankAccountId { get; set; }

        public string DateString { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string CounterPartTransactionToken { get; set; }
    }
}
