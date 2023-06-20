using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XFin.API.Core.Entities
{
    public class CostCenterAsset
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("CostCenterId")]
        public int CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<BudgetAllocation> BudgetAllocations { get; set; }
            = new List<BudgetAllocation>();

        public List<BudgetAllocation> BudgetDeallocations { get; set; }
            = new List<BudgetAllocation>();

        //TODO - check if the correct transactions are in that list
        public List<Transaction> Expenses { get; set; }
            = new List<Transaction>();

        public bool IsReserve { get; set; }

        public decimal? TargetAmount { get; set; }

        public DateTime? TargetDate { get; set; }
    }
}
