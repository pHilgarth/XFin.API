using XFin.API.Core.Enums;

namespace XFin.API.Core.Models
{
    public class RecurringTransactionCreationModel
    {
        public int SourceBankAccountId { get; set; }

        public int TargetBankAccountId { get; set; }

        public int SourceCostCenterId { get; set; }

        public int TargetCostCenterId { get; set; }

        public int ReserveId { get; set; }

        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public int Cycle { get; set; }

        public int DayOfMonth { get; set; }

        public string Reference { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
