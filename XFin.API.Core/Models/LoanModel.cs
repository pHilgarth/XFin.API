using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class LoanModel
    {
        public int Id { get; set; }

        public BankAccount CreditorBankAccount { get; set; }

        public BankAccount DebitorBankAccount { get; set; }

        public CostCenter DebitorCostCenter { get; set; }

        public CostCenter RecurringTransaction { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public int Life { get; set; }

        public double RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
