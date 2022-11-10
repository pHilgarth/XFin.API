using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<CostCenterAsset> CostCenterAssets { get; set; }
            = new List<CostCenterAsset>();

        public List<Loan> Loans { get; set; }
            = new List<Loan>();

        public List<RecurringTransaction> RecurringRevenues { get; set; }
            = new List<RecurringTransaction>();

        public List<RecurringTransaction> RecurringExpenses { get; set; }
            = new List<RecurringTransaction>();

        public List<Reserve> Reserves { get; set; }
            = new List<Reserve>();

        public List<Transaction> Revenues { get; set; }
            = new List<Transaction>();

        public List<Transaction> Expenses { get; set; }
            = new List<Transaction>();
    }
}
