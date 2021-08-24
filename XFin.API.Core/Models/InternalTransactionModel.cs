using System;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Models
{
    public class InternalTransactionModel
    {
        public int Id { get; set; }

        public string InternalBankAccountId { get; set; }

        public int TransactionCategoryId { get; set; }

        public TransactionCategoryModel TransactionCategory { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }

        public string CounterPartTransactionToken { get; set; }
    }
}
