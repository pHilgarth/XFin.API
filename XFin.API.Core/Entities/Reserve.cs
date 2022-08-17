using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class Reserve
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("CostCenterId")]
        public int CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        [Required]
        [MaxLength(25)]
        public string Reference { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
            = new List<RecurringTransaction>();
    }
}
