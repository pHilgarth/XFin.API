﻿using System;
using System.Collections.Generic;
using System.Text;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class InternalBankAccountModel : IInternalBankAccountModel
    {
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public string AccountHolderName { get; set; }

        public decimal Balance { get; set; }

        public decimal AvailableAmount { get; set; }

        public decimal ProportionPreviousMonth { get; set; }

        public string Iban { get; set; }

        public string AccountNumber { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public List<InternalTransactionModel> Revenues { get; set; }
            = new List<InternalTransactionModel>();

        public List<InternalTransactionModel> Expenses { get; set; }
            = new List<InternalTransactionModel>();
    }
}
