using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CreditorBankAccountId")]
        public int CreditorBankAccountId { get; set; }
        public BankAccount CreditorBankAccount { get; set; }

        [ForeignKey("DebitorBankAccountId")]
        public int DebitorBankAccountId { get; set; }
        public BankAccount DebitorBankAccount { get; set; }

        [ForeignKey("DebitorCostCenterId")]
        public int DebitorCostCenterId { get; set; }
        public CostCenter DebitorCostCenter { get; set; }

        [ForeignKey("RecurringTransactionId")]
        public int RecurringTransactionId { get; set; }
        public CostCenter RecurringTransaction { get; set; }

        [Required]
        [MaxLength(25)]
        public string Reference { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public int Life { get; set; }

        public double RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
