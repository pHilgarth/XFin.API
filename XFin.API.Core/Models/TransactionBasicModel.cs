using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Models
{
    public class TransactionBasicModel
    {
        public int Id { get; set; }

        public string SourceAccountHolder { get; set; }

        public string TargetAccountHolder { get; set; }

        public string CostCenterName { get; set; }

        public string CostCenterAssetName { get; set; }

        public int RecurringTransactionId { get; set; }

        public int ReserveId { get; set; }

        public int LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public bool Executed { get; set; }

        public bool IsCashTransaction { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
