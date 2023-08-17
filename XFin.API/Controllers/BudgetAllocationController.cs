using Microsoft.AspNetCore.Mvc;
using XFin.API.Core.Models;
using XFin.API.DAL.Interfaces;
namespace XFin.API.Controllers
{
    [ApiController]
    [Route("api/budgetAllocations")]
    public class BudgetAllocationController : Controller
    {
        public BudgetAllocationController(IBudgetAllocationRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{accountHolderId}")]
        public IActionResult GetSingle(int accountHolderId)
        {
            var x = 4;
            return Ok(accountHolderId + x);
        }

        [HttpPost]
        public IActionResult Create(BudgetAllocationCreationModel budgetAllocation)
        {
            var newBudgetAllocation = repo.Create(budgetAllocation);
            return newBudgetAllocation != null ? Ok(newBudgetAllocation) : BadRequest();
        }

        private readonly IBudgetAllocationRepository repo;

        
    }
}