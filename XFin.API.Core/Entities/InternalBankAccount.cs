using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class InternalBankAccount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AccountHolderId")]
        public int AccountHolderId { get; set; }
        public AccountHolder AccountHolder { get; set; }

        [Required]
        public string Iban { get; set; }

        [Required]
        public string Bic { get; set; }

        [Required]
        public string Bank { get; set; }

        public string Description { get; set; }

        public ICollection<InternalTransaction> Transactions { get; set; }
            = new List<InternalTransaction>();
    }
}
