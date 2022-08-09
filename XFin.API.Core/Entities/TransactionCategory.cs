﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<InternalTransaction> Transactions { get; set; }
            = new List<InternalTransaction>();
    }
}
