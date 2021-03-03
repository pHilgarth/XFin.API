using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class AccountHolder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
