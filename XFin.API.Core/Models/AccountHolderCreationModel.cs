namespace XFin.API.Core.Models
{
    public class AccountHolderCreationModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public bool External { get; set; }
    }
}
