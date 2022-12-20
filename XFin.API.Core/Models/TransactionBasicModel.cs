using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class TransactionBasicModel
    {
        public int Id { get; set; }

        public string SourceBankAccountString { get; set; }

        public string TargetBankAccountString { get; set; }

        public string SourceCostCenterString { get; set; }

        public string TargetCostCenterString { get; set; }

        public int RecurringTransactionId { get; set; }

        public int ReserveId { get; set; }

        public int LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
