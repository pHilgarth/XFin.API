using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class ExternalTransaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ExternalBankAccountId")]
        public int ExternalBankAccountId { get; set; }
        public ExternalBankAccount ExternalBankAccount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string TransactionToken { get; set; }

        public string CounterPartTransactionToken { get; set; }

    }
}
