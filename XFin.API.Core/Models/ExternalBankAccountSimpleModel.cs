namespace XFin.API.Core.Models
{
    public class ExternalBankAccountSimpleModel
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public int ExternalPartyId { get; set; }

        public string ExternalPartyName { get; set; }
    }
}
