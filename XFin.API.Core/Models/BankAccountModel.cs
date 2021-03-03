using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class BankAccountModel
    {
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public decimal Balance { get; set; }

        public string AccountNumber { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string AccountType { get; set; }
    }
}
