using System;

namespace XFin.API.Core.Models
{
    public class BudgetAllocationBasicModel
    {
        public int Id { get; set; }

        public int? BankAccountId { get; set; }

        public int? SourceCostCenterId { get; set; }

        public int? TargetCostCenterId { get; set; }

        public int? SourceCostCenterAssetId { get; set; }

        public int? TargetCostCenterAssetId { get; set; }

        public int? RecurringBudgetAllocationId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
