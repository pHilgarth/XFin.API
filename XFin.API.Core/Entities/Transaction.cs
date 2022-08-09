using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public int SourceBankAccountId { get; set; }
        public BankAccount SourceBankAccount { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public int TargetBankAccountId { get; set; }
        public BankAccount TargetBankAccount { get; set; }

        [ForeignKey("SourceCostCenterId")]
        public int SourceCostCenterId { get; set; }
        public CostCenter SourceCostCenter { get; set; }

        [ForeignKey("TargetCostCenterId")]
        public int TargetCostCenterId { get; set; }
        public CostCenter TargetCostCenter { get; set; }

        [ForeignKey("RecurringTransactionId")]
        public int RecurringTransactionId { get; set; }
        public RecurringTransaction RecurringTransaction { get; set; }

        [ForeignKey("ReserveId")]
        public int ReserveId { get; set; }
        public Reserve Reserve { get; set; }

        [ForeignKey("LoanId")]
        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        [Required]
        [MaxLength(25)]
        public string Reference { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public List<Transactions> Transactions { get; set; }
            = new List<Transactions>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
    }
}
