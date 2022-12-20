namespace XFin.API.Core.Models
{
    public class CostCenterAssetCreationModel
    {
        public string Name { get; set; }

        public int BankAccountId { get; set; }

        public int CostCenterId { get; set; }

        public decimal? Amount { get; set; }
    }
}
