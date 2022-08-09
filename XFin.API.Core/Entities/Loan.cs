using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("InternalBankAccountId")]
        public int InternalBankAccountId { get; set; }
        public InternalBankAccount InternalBankAccount { get; set; }

        [Required]
        [MaxLength(25)]
        public string Reference { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public int Life { get; set; }

        public decimal RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }
    }
}
