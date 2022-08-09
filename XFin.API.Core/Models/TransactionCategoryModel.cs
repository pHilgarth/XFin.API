using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class CostCenterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal ProportionPreviousMonth { get; set; }

        public decimal RevenuesTotal { get; set; }

        public decimal InternalTransfersAmount { get; set; }

        public decimal Budget { get; set; }

        public decimal ExpensesTotal { get; set; }

        public List<InternalTransactionModel> Revenues { get; set; }
            = new List<InternalTransactionModel>();

        public List<InternalTransactionModel> Expenses { get; set; }
            = new List<InternalTransactionModel>();
    }
}
