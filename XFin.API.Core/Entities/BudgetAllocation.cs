using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class BudgetAllocation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("SourceCostCenterId")]
        public int? SourceCostCenterId { get; set; }
        public CostCenter SourceCostCenter { get; set; }

        [ForeignKey("TargetCostCenterId")]
        public int? TargetCostCenterId { get; set; }
        public CostCenter TargetCostCenter { get; set; }

        [ForeignKey("SourceCostCenterAssetId")]
        public int? SourceCostCenterAssetId { get; set; }
        public CostCenterAsset SourceCostCenterAsset { get; set; }

        [ForeignKey("TargetCostCenterAssetId")]
        public int? TargetCostCenterAssetId { get; set; }
        public CostCenterAsset TargetCostCenterAsset { get; set; }

        [ForeignKey("RecurringBudgetAllocationId")]
        public int? RecurringBudgetAllocationId { get; set; }
        public RecurringBudgetAllocation RecurringBudgetAllocatioin { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        //equals property 'Date' on non-recurring transactions - does NOT equal property 'Date' on transactions, that are linked to a recurringTransaction!
        public DateTime DueDate { get; set; }

        //always true on non-recurring regular transactions - can be false on recurringTransactions, that have been cancelled by the user 
        public bool Executed { get; set; }
    }
}
