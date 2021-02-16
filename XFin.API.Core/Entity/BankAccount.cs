using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class BankAccount
    {
        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        [Key]
        public int Id { get; set; }

        [ForeignKey("DepositorId")]
        public int DepositorId { get; set; }
        public Depositor Depositor { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string Iban { get; set; }

        [Required]
        public string Bic { get; set; }

        [Required]
        public string Bank { get; set; }

        [Required]
        public string AccountType { get; set; }
    }
}
