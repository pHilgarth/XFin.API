namespace XFin.API.Core.Models
{
    public class BudgetAllocationCreationModel
    {
        public int? BankAccountId { get; set; }

        public int? SourceCostCenterId { get; set; }

        public int? TargetCostCenterId { get; set; }

        public int? SourceCostCenterAssetId { get; set; }

        public int? TargetCostCenterAssetId { get; set; }

        public int? RecurringBudgetAllocationId { get; set; }

        public decimal Amount { get; set; }

        public string DateString { get; set; }

        public string DueDateString { get; set; }

        public bool Executed { get; set; }
    }
}