namespace XFin.API.Core.Models
{
    public class ExternalTransactionCreationModel
    {
        public int ExternalBankAccountId { get; set; }

        public string DateString { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string TransactionToken { get; set; }

        public string CounterPartTransactionToken { get; set; }
    }
}
