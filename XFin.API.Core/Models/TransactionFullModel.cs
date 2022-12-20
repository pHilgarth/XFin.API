using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class TransactionFullModel
    {
        public int Id { get; set; }

        public BankAccountModel SourceBankAccount { get; set; }

        public BankAccountModel TargetBankAccount { get; set; }

        public CostCenterModel SourceCostCenter { get; set; }

        public CostCenterModel TargetCostCenter { get; set; }

        public int RecurringTransactionId { get; set; }

        public int ReserveId { get; set; }

        public int LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
