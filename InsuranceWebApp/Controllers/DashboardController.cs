using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceWebApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly InsuranceDbContext _context;

        public DashboardController(InsuranceDbContext context)
        {
            _context = context;
        }

        // GET: api/Dashboard/employees/{userId}
        [HttpGet("employees/{userId}")]
        public ActionResult<EmployeeDetail> GetEmployee(string userId)
        {
            int parsedUserId;
            if (int.TryParse(userId, out parsedUserId))
            {
                var employee = _context.EmployeeDetails.Include(e => e.User)
                                                       .FirstOrDefault(e => e.User.UserId == parsedUserId);

                if (employee == null)
                {
                    return NotFound(); // Return NotFound if employee is null
                }

                return Ok(employee);
            }

            return BadRequest("Invalid user ID format.");
        }

        [HttpGet("policies/{userId}")]
        public ActionResult<IEnumerable<PolicyDetail>> GetPolicies(string userId)
        {
            int parsedUserId;
            if (int.TryParse(userId, out parsedUserId))
            {
                var policies = _context.PolicyDetails.Include(p => p.User)
                                                      .Where(p => p.User.UserId == parsedUserId)
                                                      .ToList();

                return Ok(policies);
            }

            return BadRequest("Invalid user ID format.");
        }
    }
}
