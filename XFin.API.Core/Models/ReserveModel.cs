using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class ReserveModel
    {
        public int Id { get; set; }

        public BankAccountModel BankAccount { get; set; }

        public CostCenterModel CostCenter { get; set; }

        public string Reference { get; set; }

        public decimal? TargetAmount { get; set; }

        public DateTime? TargetDate { get; set; }

        public List<TransactionBasicModel> Revenues { get; set; }
            = new List<TransactionBasicModel>();

        public List<TransactionBasicModel> TransferRevenues { get; set; }
            = new List<TransactionBasicModel>();

        public List<TransactionBasicModel> Expenses { get; set; }
            = new List<TransactionBasicModel>();

        public List<TransactionBasicModel> TransferExpenses { get; set; }
            = new List<TransactionBasicModel>();

        public List<TransactionBasicModel> RecurringTransactions { get; set; }
            = new List<TransactionBasicModel>();
    }
}
