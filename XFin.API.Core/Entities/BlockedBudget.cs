using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class BlockedBudget
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TransactionCategoryId")]
        public int TransactionCategoryId { get; set; }

        public TransactionCategory TransactionCategory { get; set; }

        [ForeignKey("InternalBankAccountId")]
        public int InternalBankAccountId { get; set; }

        public InternalBankAccount InternalBankAccount { get; set; }

        public decimal? Amount { get; set; }

        public string Reference { get; set; }
    }
}
