using System;

namespace XFin.API.Core.Models
{
    public class InternalTransactionModel
    {
        public int Id { get; set; }

        public string InternalBankAccountId { get; set; }

        public string InternalBankAccountIban { get; set; }

        public string CounterParty { get; set; }

        public int TransactionCategoryId { get; set; }

        public string TransactionCategoryName { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }
    }
}
