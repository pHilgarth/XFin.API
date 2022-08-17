using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class BankAccountUpdateModel
    {
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public bool External { get; set; }
    }
}
