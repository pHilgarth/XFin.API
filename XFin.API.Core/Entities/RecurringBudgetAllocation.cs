using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class RecurringBudgetAllocation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("SourceCostCenterId")]
        public int SourceCostCenterId { get; set; }
        public CostCenter SourceCostCenter { get; set; }

        [ForeignKey("TargetCostCenterId")]
        public int TargetCostCenterId { get; set; }
        public CostCenter TargetCostCenter { get; set; }

        [ForeignKey("SourceCostCenterAssetId")]
        public int? SourceCostCenterAssetId { get; set; }
        public CostCenterAsset SourceCostCenterAsset { get; set; }

        [ForeignKey("TargetCostCenterAssetId")]
        public int? TargetCostCenterAssetId { get; set; }
        public CostCenterAsset TargetCostCenterAsset { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Range(1, 12)]
        public int Cycle { get; set; }

        [Required]
        [Range(1, 28)]
        public int DayOfMonth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<BudgetAllocation> BudgetAllocations { get; set; }
            = new List<BudgetAllocation>();
    }
}
