namespace XFin.API.Core.Models
{
    public class AccountHolderUpdateModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public bool External { get; set; }
    }
}
