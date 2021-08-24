using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class InternalBankAccountCreationModel
    {
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
