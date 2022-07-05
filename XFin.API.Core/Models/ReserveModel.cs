using System;
using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class ReserveModel
    {
        public int Id { get; set; }

        public int InternalBankAccountId { get; set; }

        public decimal Amount { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public string Reference { get; set; }

        public List<InternalTransactionModel> PaymentReceipts { get; set; }
            = new List<InternalTransactionModel>();
    }
}
