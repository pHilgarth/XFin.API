using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;

namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public IActionResult Create(UserCreationModel user)
        {
            var newUser = repo.Create(user);
            return newUser != null ? Ok(newUser) : BadRequest();
        }


        [HttpGet("{email}/{password}")]
        public IActionResult Get(string email, string password)
        {
            var user = repo.Get(email, password);

            return user != null ? Ok(user) : NotFound();
        }

        private readonly IUserRepository repo;
    }
}