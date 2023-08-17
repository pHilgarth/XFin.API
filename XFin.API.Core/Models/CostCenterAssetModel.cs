using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class CostCenterAssetModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal AllocationBalanceCurrentMonth { get; set; }

        public decimal Balance { get; set; }

        public decimal BalancePreviousMonth { get; set; }

        public decimal ExpensesSum { get; set; }
    }
}
