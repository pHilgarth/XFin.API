using System;

namespace XFin.API.Core.Models
{
    public class ReserveUpdateModel
    {
        public int Id { get; set; }

        public int InternalBankAccountId { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public decimal TargetAmount { get; set; }

        public string TargetDate { get; set; }
    }
}
