
using AgriConnect.Data;
using AgriConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AgriConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Get All Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.UsersAgri.ToListAsync();
            return Ok(users);
        }
        //Add user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User newUser)
        {
            if (string.IsNullOrEmpty(newUser.Name))
            {
                return BadRequest(new {message ="Name is a must"});
            }
            await _context.UsersAgri.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddUser), new { id = newUser.Id }, newUser);
        }
    }
}
