namespace XFin.API.Core.Models
{
    public class InternalTransactionCreationModel
    {
        public int InternalBankAccountId { get; set; }

        public int CostCenterId { get; set; }

        public string DateString { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string TransactionToken { get; set; }

        public string CounterPartTransactionToken { get; set; }
    }
}
