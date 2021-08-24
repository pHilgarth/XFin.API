using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class ExternalBankAccountCreationModel
    {
        public string Iban { get; set; }

        public string Bic { get; set; }

        public int ExternalPartyId { get; set; }
    }
}
