using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class BankAccountIdentifier
    {
        [Key]
        [Required]
        public string Iban { get; set; }

        [Required]
        public string Bic { get; set; }
    }
}