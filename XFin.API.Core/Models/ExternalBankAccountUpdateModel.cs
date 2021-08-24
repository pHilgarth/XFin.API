using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class ExternalBankAccountUpdateModel
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }
    }
}
