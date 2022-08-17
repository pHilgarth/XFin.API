using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Models
{
    public class RecurringTransactionModel
    {
        public int Id { get; set; }

        public BankAccount SourceBankAccount { get; set; }

        public BankAccount TargetBankAccount { get; set; }

        public CostCenter SourceCostCenter { get; set; }

        public CostCenter TargetCostCenter { get; set; }

        public Reserve Reserve { get; set; }

        public Loan Loan { get; set; }

        public decimal Amount { get; set; }

        public int Cycle { get; set; }

        public int DayOfMonth { get; set; }

        public string Reference { get; set; }

        public TransactionType TransactionType { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
