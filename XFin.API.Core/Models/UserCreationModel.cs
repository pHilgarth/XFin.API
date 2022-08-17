using System;

namespace XFin.API.Core.Models
{
    public class UserCreationModel
    {
        public string Email { get; set; }

        //TODO - password needs to be encrypted, and some validation must take place, until then I just store it as is
        public string Password { get; set; }

    }
}
