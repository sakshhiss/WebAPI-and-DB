using InsuranceWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InsuranceWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly InsuranceDbContext _context;

        public LoginController(InsuranceDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest model)
        {
            var user = _context.UserLoginDetails.SingleOrDefault(u => u.EmailId == model.Email);

            if (user == null)
            {
                return NotFound("User not found"); // Return 404 if user not found
            }

            if (user.Password != model.Password)
            {
                return BadRequest("Incorrect password"); // Return 400 if password is incorrect
            }

            return Ok(user); // Return user details if authentication successful
        }
    }
}
