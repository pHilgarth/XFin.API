using System;

//TODO - I don't think, that I need this...
namespace XFin.API.Core.Models
{
    public class TransactionUpdateModel
    {
        public int Id { get; set; }

        public int SourceBankAccountId { get; set; }

        public int TargetBankAccountId { get; set; }

        public int CostCenterId { get; set; }

        public int CostCenterAssetId { get; set; }

        public int RecurringTransactionId { get; set; }

        public int ReserveId { get; set; }

        public int LoanId { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
