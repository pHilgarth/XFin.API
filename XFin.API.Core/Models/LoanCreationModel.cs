namespace XFin.API.Core.Models
{
    public class LoanCreationModel
    {
        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public int Life { get; set; }

        public decimal RateOfInterest { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public int InternalBankAccountId { get; set; }
    }
}
