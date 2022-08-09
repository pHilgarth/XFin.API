using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class AccountHolder
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public bool External { get; set; }

        public List<BankAccount> BankAccounts { get; set; }
    }
}
