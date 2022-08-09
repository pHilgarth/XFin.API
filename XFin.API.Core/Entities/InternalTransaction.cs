﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class InternalTransaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("InternalBankAccountId")]
        public int InternalBankAccountId { get; set; }
        public InternalBankAccount InternalBankAccount { get; set; }

        [ForeignKey("CostCenterId")]
        public int CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string TransactionToken { get; set; }

        public string CounterPartTransactionToken { get; set; }

    }
}
