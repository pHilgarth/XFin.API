using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

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
