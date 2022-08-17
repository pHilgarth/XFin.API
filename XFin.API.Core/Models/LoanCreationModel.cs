using System;

namespace XFin.API.Core.Models
{
    public class LoanCreationModel
    {
        public int CreditorBankAccountId { get; set; }

        public int DebitorBankAccountId { get; set; }

        public int DebitorCostCenterId { get; set; }

        public int RecurringTransactionId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public int Life { get; set; }

        public double RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }
    }
}
