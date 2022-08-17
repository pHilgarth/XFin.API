using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        //TODO - password needs to be encrypted, and some validation must take place, until then I just store it as is
        public string Password { get; set; }

        public List<AccountHolder> AccountHolders { get; set; }

    }
}
