using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class BankAccount
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

        public string Bank { get; set; }

        [MaxLength(25)]
        public string Description { get; set; }

        public bool External { get; set; }

        public List<Loan> Loans { get; set; }
            = new List<Loan>();

        public List<RecurringTransaction> RecurringTransactions { get; set; }
            = new List<RecurringTransaction>();

        public List<Reserve> Reserves { get; set; }
            = new List<Reserve>();

        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
