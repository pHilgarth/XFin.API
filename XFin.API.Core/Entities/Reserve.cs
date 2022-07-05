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

        [ForeignKey("InternalBankAccountId")]
        public int InternalBankAccountId { get; set; }
        public InternalBankAccount InternalBankAccount { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        [Required]
        public string Reference { get; set; }

        public List<InternalTransaction> Transactions { get; set; }
            = new List<InternalTransaction>();

        public List<RecurringRevenue> SavingRatesExternal { get; set; }
            = new List<RecurringRevenue>();

        public List<RecurringTransfer> SavingRatesInternal { get; set; }
            = new List<RecurringTransfer>();
    }
}
