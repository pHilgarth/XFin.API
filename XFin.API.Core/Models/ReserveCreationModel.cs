﻿using System;

namespace XFin.API.Core.Models
{
    public class ReserveCreationModel
    {
        public int BankAccountId { get; set; }

        public int CostCenterId { get; set; }

        public string Reference { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }
    }
}