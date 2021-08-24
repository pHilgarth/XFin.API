using System;
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

        [ForeignKey("TransactionCategoryId")]
        public int TransactionCategoryId { get; set; }
        public TransactionCategory TransactionCategory { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Reference { get; set; }

        public string CounterPartTransactionToken { get; set; }

    }
}
