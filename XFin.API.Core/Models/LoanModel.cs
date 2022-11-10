using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class LoanModel
    {
        public int Id { get; set; }

        public BankAccountModel CreditorBankAccount { get; set; }

        public BankAccountModel DebitorBankAccount { get; set; }

        public CostCenterModel DebitorCostCenter { get; set; }

        public CostCenterModel RecurringTransaction { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public int Life { get; set; }

        public double RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public List<TransactionModel> Transactions { get; set; }
            = new List<TransactionModel>();
    }
}
