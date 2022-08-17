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

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<UserUpdateModel> userPatch)
        {
            //TODO - error handling
            //TODO - check if this variable name is correct - its on accountHolderController and variable is called updatedBankAccout??
            var updatedBankAccount = repo.Update(id, userPatch);

            return updatedBankAccount != null ? Ok(updatedBankAccount) : NotFound();
        }

        private readonly IUserRepository repo;
    }
}