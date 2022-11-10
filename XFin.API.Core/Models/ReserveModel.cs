using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class ReserveModel
    {
        public int Id { get; set; }

        public BankAccountModel BankAccount { get; set; }

        public CostCenterModel CostCenter { get; set; }

        public string Reference { get; set; }

        public decimal? TargetAmount { get; set; }

        public DateTime? TargetDate { get; set; }

        public List<TransactionModel> Revenues { get; set; }
            = new List<TransactionModel>();

        public List<TransactionModel> Expenses { get; set; }
            = new List<TransactionModel>();

        public List<RecurringTransactionModel> RecurringTransactions { get; set; }
            = new List<RecurringTransactionModel>();
    }
}
