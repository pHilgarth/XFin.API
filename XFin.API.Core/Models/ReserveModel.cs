using System;
using System.Collections.Generic;

namespace XFin.API.Core.Models
{
    public class ReserveModel
    {
        public int Id { get; set; }

        public int InternalBankAccountId { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public decimal TargetAmount { get; set; }

        public string TargetDate { get; set; }

        public List<InternalTransactionModel> Transactions { get; set; }
            = new List<InternalTransactionModel>();
    }
}
