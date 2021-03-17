using System;
using System.Collections.Generic;
using System.Text;

namespace XFin.API.Core.Models
{
    public class ExternalPartyModel
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Name { get; set; }
    }
}
