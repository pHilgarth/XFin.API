﻿namespace XFin.API.Core.Models
{
    public class TransactionCreationModel
    {
        public int? SourceBankAccountId { get; set; }

        public int? TargetBankAccountId { get; set; }

        public int? CostCenterId { get; set; }

        public int? CostCenterAssetId { get; set; }

        public int? RecurringTransactionId { get; set; }

        public int? LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public string DueDateString { get; set; }

        public string DateString { get; set; }

        public string TransactionType { get; set; }

        public bool Executed { get; set; }

        public bool IsCashTransaction { get; set; }
    }
}
