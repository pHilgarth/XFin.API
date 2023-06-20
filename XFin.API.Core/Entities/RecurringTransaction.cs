using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class RecurringTransaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public int SourceBankAccountId { get; set; }
        public BankAccount SourceBankAccount { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public int TargetBankAccountId { get; set; }
        public BankAccount TargetBankAccount { get; set; }

        [ForeignKey("LoanId")]
        public int? LoanId { get; set; }
        public Loan Loan { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Reference { get; set; }

        [Required]
        [Range(1, 12)]
        public int Cycle { get; set; }

        [Required]
        [Range(1, 28)]
        public int DayOfMonth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TransactionType TransactionType { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
