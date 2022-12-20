using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class CostCenterAsset
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("CostCenterId")]
        public int CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Amount { get; set; }

        //TODO - I'm not sure, if I need these properties for a CostCenterAsset:

        //public List<Transaction> Revenues { get; set; }
        //    = new List<Transaction>();

        //public List<Transaction> Expenses { get; set; }
        //    = new List<Transaction>();

        //public List<Loan> Loans { get; set; }
        //    = new List<Loan>();

        //public List<RecurringTransaction> RecurringRevenues { get; set; }
        //    = new List<RecurringTransaction>();

        //public List<RecurringTransaction> RecurringExpenses { get; set; }
        //    = new List<RecurringTransaction>();

        //public List<Reserve> Reserves { get; set; }
        //    = new List<Reserve>();


    }
}
