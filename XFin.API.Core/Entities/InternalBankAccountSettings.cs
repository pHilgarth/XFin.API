using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XFin.API.Core.Entities
{
    public class InternalBankAccountSettings
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("InternalBankAccountId")]
        public int InternalBankAccountId { get; set; }
        public InternalBankAccount InternalBankAccount { get; set; }

        public bool EffectsExpenses { get; set; }
        public bool ReceivesRevenues { get; set; }
        public bool AllowsOverdraft { get; set; }
        public decimal? BalanceThreshold { get; set; }
        public decimal? ExpensesThreshold { get; set; }

    }
}
