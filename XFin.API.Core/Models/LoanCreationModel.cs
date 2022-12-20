using System;

namespace XFin.API.Core.Models
{
    public class LoanCreationModel
    {
        public int CreditorBankAccountId { get; set; }

        public int DebitorBankAccountId { get; set; }

        //TODO - I don't think, that I need this. I have a loan id in the recurringTransaction object
        //public int? RecurringTransactionId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public DateTime StartDate { get; set; }

        public int? Life { get; set; }

        public double? RateOfInterest { get; set; }

        public decimal? MonthlyInstallment { get; set; }
    }
}
