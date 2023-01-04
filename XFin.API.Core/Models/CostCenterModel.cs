using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class CostCenterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal BalancePreviousMonth { get; set; }

        public decimal RevenuesSum { get; set; }

        public decimal ExpensesSum { get; set; }

        public decimal TransferSum { get; set; }

        public List<CostCenterAssetModel> CostCenterAssets { get; set; }
            = new List<CostCenterAssetModel>();

        public List<ReserveSimpleModel> Reserves { get; set; }
            = new List<ReserveSimpleModel>();

        //TODO - do I need this?
        //public List<RecurringTransactionModel> RecurringTransactions { get; set; }
        //    = new List<RecurringTransactionModel>();



        //TODO - do I need this?
        //public List<TransactionBasicModel> Transactions { get; set; }
        //    = new List<TransactionBasicModel>();
    }
}
