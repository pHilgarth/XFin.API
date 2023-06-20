using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<CostCenterAsset> CostCenterAssets { get; set; }
            = new List<CostCenterAsset>();

        public List<BudgetAllocation> BudgetAllocations { get; set; }
            = new List<BudgetAllocation>();

        public List<BudgetAllocation> BudgetDeallocations { get; set; }
            = new List<BudgetAllocation>();

        //TODO - check if the correct expenses are in this list
        public List<Transaction> Expenses { get; set; }
            = new List<Transaction>();

        //TODO - remove if not needed
        //public List<RecurringBudgetAllocation> RecurringBudgetAllocations { get; set; }
        //    = new List<RecurringBudgetAllocation>();

        //public List<Reserve> Reserves { get; set; }
        //    = new List<Reserve>();


    }
}
