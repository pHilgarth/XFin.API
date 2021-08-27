using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class ExternalBankAccount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ExternalPartyId")]
        public int ExternalPartyId { get; set; }
        public ExternalParty ExternalParty { get; set; }

        [Required]
        public string Iban { get; set; }

        [Required]
        public string Bic { get; set; }

        public List<ExternalTransaction> Transactions { get; set; }
            = new List<ExternalTransaction>();

    }
}
