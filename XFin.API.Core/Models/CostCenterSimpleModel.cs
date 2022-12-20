using System.Collections.Generic;

//TODO - I'm not using this class atm
namespace XFin.API.Core.Models
{
    public class CostCenterSimpleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
