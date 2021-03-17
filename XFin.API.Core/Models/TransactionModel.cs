using System;
using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string SourceAccountNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public ExternalPartyModel ExternalParty { get; set; }
        public TransactionCategoryModel TransactionCategory { get; set; }

    }
}
