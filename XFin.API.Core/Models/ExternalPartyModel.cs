namespace XFin.API.Core.Models
{
    public class ExternalPartyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ExternalBankAccountModel ExternalBankAccount { get; set; }
    }
}
