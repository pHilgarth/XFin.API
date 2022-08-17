using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public BankAccount SourceBankAccount { get; set; }

        public BankAccount TargetBankAccount { get; set; }

        public CostCenter SourceCostCenter { get; set; }

        public CostCenter TargetCostCenter { get; set; }

        public RecurringTransaction RecurringTransaction { get; set; }

        public Reserve Reserve { get; set; }

        public Loan Loan { get; set; }

        public string Reference { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
            = new List<RecurringTransaction>();
    }
}
