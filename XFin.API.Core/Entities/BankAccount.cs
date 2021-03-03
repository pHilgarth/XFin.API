﻿using System.Collections.Generic;
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

        [ForeignKey("BankAccountIban")]
        public string BankAccountIban { get; set; }
        public BankAccountIdentifier BankAccountIdentifier { get; set; }


        [Required]
        public string Bank { get; set; }

        [Required]
        public string AccountType { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
            = new List<Transaction>();

    }
}
