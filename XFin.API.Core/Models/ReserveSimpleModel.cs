using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class ReserveSimpleModel
    {
        public int Id { get; set; }

        public string Reference { get; set; }

        public decimal Balance { get; set; }

        public decimal? TargetAmount { get; set; }

        public DateTime? TargetDate { get; set; }
    }
}
