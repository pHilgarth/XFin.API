using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //TODO - password needs to be encrypted, and some validation must take place, until then I just store it as is
        [Required]
        public string Password { get; set; }

        public List<AccountHolder> AccountHolders { get; set; }
    }
}
