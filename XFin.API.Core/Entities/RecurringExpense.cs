using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class RecurringExpense
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public int SourceBankAccountId { get; set; }
        public InternalBankAccount SourceBAnkAccount { get; set; }

        [ForeignKey("ExternalPartyId")]
        public int ExternalPartyId { get; set; }
        //TODO - wtf, this is just wrong
        public InternalBankAccount ExternalParty { get; set; }

        [ForeignKey("SourceCostCenterId")]
        public int SourceCostCenterId { get; set; }
        public CostCenter SourceCostCenter { get; set; }

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
