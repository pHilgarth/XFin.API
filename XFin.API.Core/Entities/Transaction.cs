using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public int? SourceBankAccountId { get; set; }
        public BankAccount SourceBankAccount { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public int? TargetBankAccountId { get; set; }
        public BankAccount TargetBankAccount { get; set; }

        [ForeignKey("SourceCostCenterId")]
        public int? SourceCostCenterId { get; set; }
        public CostCenter SourceCostCenter { get; set; }

        [ForeignKey("TargetCostCenterId")]
        public int? TargetCostCenterId { get; set; }
        public CostCenter TargetCostCenter { get; set; }

        [ForeignKey("RecurringTransactionId")]
        public int? RecurringTransactionId { get; set; }
        public RecurringTransaction RecurringTransaction { get; set; }

        [ForeignKey("ReserveId")]
        public int? ReserveId { get; set; }
        public Reserve Reserve { get; set; }

        [ForeignKey("LoanId")]
        public int? LoanId { get; set; }
        public Loan Loan { get; set; }

        [MaxLength(100)]
        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        //equals property 'Date' on non-recurring transactions - does NOT equal property 'Date' on transactions, that are linked to a recurringTransaction!
        public DateTime DueDate { get; set; }

        //always true on non-recurring regular transactions - can be false on recurringTransactions, that have been cancelled by the user 
        public bool Executed { get; set; }

        public bool IsCashTransaction { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
