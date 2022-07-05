using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class RecurringRevenue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ExternalPartyId")]
        public int ExternalPartyId { get; set; }
        public ExternalParty ExternalParty { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public int TargetBankAccountId { get; set; }
        public InternalBankAccount TargetBankAccount { get; set; }

        [ForeignKey("TargetTransactionCategoryId")]
        public int TargetTransactionCategoryId { get; set; }
        public TransactionCategory TargetTransactionCategory { get; set; }

        [Required]
        public int Cycle { get; set; }

        [Required]
        public int DayOfMonth { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
