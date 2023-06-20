using System;
using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class RecurringTransactionModel
    {
        public int Id { get; set; }

        public BankAccountModel SourceBankAccount { get; set; }

        public BankAccountModel TargetBankAccount { get; set; }

        public CostCenterModel SourceCostCenter { get; set; }

        public CostCenterModel TargetCostCenter { get; set; }

        public LoanModel Loan { get; set; }

        public decimal Amount { get; set; }

        public int Cycle { get; set; }

        public int DayOfMonth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Reference { get; set; }

        public string TransactionType { get; set; }

        public List<TransactionFullModel> Transactions { get; set; }
            = new List<TransactionFullModel>();
    }
}
