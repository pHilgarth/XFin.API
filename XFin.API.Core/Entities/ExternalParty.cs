using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XFin.API.Core.Entities
{
    public class ExternalParty
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountIdentifierIban")]
        public string BankAccountIdentifierIban { get; set; }
        public BankAccountIdentifier BankAccountIdentifier { get; set; }

        public string Name { get; set; }
    }
}
