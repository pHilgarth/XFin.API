namespace XFin.API.Core.Models
{
    public class InternalBankAccountSimpleModel : IInternalBankAccountModel
    {
        //TODO - check if this class is used or if it can be deleted
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public string AccountHolderName { get; set; }

        public string Iban { get; set; }

        public string AccountNumber { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
