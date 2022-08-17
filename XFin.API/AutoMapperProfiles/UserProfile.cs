using System.Collections.Generic;
using AutoMapper;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;

namespace XFin.API.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreationModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}
