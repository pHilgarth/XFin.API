using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Models
{
    public class RecurringTransactionModel
    {
        public int Id { get; set; }

        public BankAccountModel SourceBankAccount { get; set; }

        public BankAccountModel TargetBankAccount { get; set; }

        public CostCenterModel SourceCostCenter { get; set; }

        public CostCenterModel TargetCostCenter { get; set; }

        public ReserveModel Reserve { get; set; }

        public LoanModel Loan { get; set; }

        public decimal Amount { get; set; }

        public int Cycle { get; set; }

        public int DayOfMonth { get; set; }

        public string Reference { get; set; }

        public TransactionType TransactionType { get; set; }

        public List<TransactionModel> Transactions { get; set; }
            = new List<TransactionModel>();
    }
}
