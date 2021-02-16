using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class Depositor
    {
        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
            = new List<BankAccount>();
    }
}
