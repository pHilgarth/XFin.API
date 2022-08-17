using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class ReserveModel
    {
        public int Id { get; set; }

        public BankAccount BankAccount { get; set; }

        public CostCenter CostCenter { get; set; }

        public string Reference { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
            = new List<RecurringTransaction>();
    }
}
