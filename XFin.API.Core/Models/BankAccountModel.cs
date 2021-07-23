using System;
using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class BankAccountModel
    {
        public string AccountNumber { get; set; }

        public int AccountHolderId { get; set; }

        public string AccountHolderName { get; set; }

        public decimal Balance { get; set; }

        public decimal ProportionPreviousMonth { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public ICollection<TransactionModel> Revenues { get; set; }

        public ICollection<TransactionModel> Expenses { get; set; }
    }
}
