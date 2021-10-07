namespace XFin.API.Core.Models
{
    public class InternalBankAccountCreationModel
    {
        public int AccountHolderId { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}