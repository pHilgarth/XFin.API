using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IMapper mapper, XFinDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private readonly IMapper mapper;
        private readonly XFinDbContext context;

        public UserModel Create(UserCreationModel user)
        {
            var newUser = mapper.Map<User>(user);

            context.Users.Add(newUser);
            context.SaveChanges();

            return mapper.Map<UserModel>(newUser);
        }

        public UserModel Get(string email, string password)
        {
            return mapper.Map<UserModel>(context.Users
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefault());
        }
    }
}