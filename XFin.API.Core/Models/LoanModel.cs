using System;

namespace XFin.API.Core.Models
{
    public class LoanModel
    {
        public int Id { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public int Life { get; set; }

        public decimal RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public InternalBankAccountModel BankAccount { get; set; }
    }
}
