using System;

namespace XFin.API.Core.Models
{
    public class TransactionCreationModel
    {
        public int? SourceBankAccountId { get; set; }

        public int TargetBankAccountId { get; set; }

        public int? SourceCostCenterId { get; set; }

        public int TargetCostCenterId { get; set; }

        public int? RecurringTransactionId { get; set; }

        public int? ReserveId { get; set; }

        public int? LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public string DateString { get; set; }
    }
}
