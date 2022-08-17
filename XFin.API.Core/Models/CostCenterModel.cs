using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class CostCenterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Loan> Loans { get; set; }
            = new List<Loan>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
            = new List<RecurringTransaction>();

        public List<Reserve> Reserves { get; set; }
            = new List<Reserve>();

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
