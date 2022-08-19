using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class CostCenterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LoanModel> Loans { get; set; }
            = new List<LoanModel>();

        public List<RecurringTransactionModel> RecurringTransactions { get; set; }
            = new List<RecurringTransactionModel>();

        public List<ReserveModel> Reserves { get; set; }
            = new List<ReserveModel>();

        public List<TransactionModel> Transactions { get; set; }
            = new List<TransactionModel>();
    }
}
