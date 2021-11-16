using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class TransactionCategorySimpleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BlockedBudget> BlockedBudget { get; set; }
            = new List<BlockedBudget>();
    }
}
