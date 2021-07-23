using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class TransactionCreationModel
    {
        public string BankAccountNumber { get; set; }

        public int TransactionCategoryId { get; set; }

        public string DateString { get; set; }

        public decimal Amount { get; set; }

        public string Reference { get; set; }
    }
}
