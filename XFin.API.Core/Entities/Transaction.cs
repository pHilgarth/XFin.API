using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("TransactionCategoryId")]
        public int TransactionCategoryId { get; set; }
        public TransactionCategory TransactionCategory { get; set; }

        [ForeignKey("ExternalPartyId")]
        public int? ExternalPartyId { get; set; }
        public ExternalParty ExternalParty { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Reference { get; set; }

        public string CounterPartTransactionToken { get; set; }

    }
}
