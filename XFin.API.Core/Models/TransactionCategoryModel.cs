using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class TransactionCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal ProportionPreviousMonth { get; set; }

        public decimal RevenuesTotal { get; set; }

        public decimal Budget { get; set; }

        public decimal ExpensesTotal { get; set; }

        public ICollection<InternalTransactionModel> Revenues { get; set; }

        public ICollection<InternalTransactionModel> Expenses { get; set; }
    }
}
