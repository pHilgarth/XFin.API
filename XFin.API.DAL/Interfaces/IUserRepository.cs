﻿using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Models;

namespace XFin.API.DAL.Interfaces
{
    public interface IUserRepository
    {
        UserModel Create(UserCreationModel user);
        UserModel Get(string email, string password);
    }
}
