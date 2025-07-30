using AgriConnect.Data;
using AgriConnect.Dtos;
using AgriConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperativesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CooperativesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cooperatives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cooperative>>> GetCooperatives()
        {
            return await _context.Cooperatives
                .Include(c => c.Leader)
                .Include(c => c.Members)
                .Include(c => c.Orders)
                .ToListAsync();
        }

        // GET: api/Cooperatives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cooperative>> GetCooperative(int id)
        {
            var cooperative = await _context.Cooperatives
                .Include(c => c.Leader)
                .Include(c => c.Members)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cooperative == null)
            {
                return NotFound();
            }

            return cooperative;
        }

        // POST: api/Cooperatives
        [HttpPost]
        public async Task<IActionResult> CreateCooperative([FromBody] CreateCooperativeDto dto)
        {
            var leader = await _context.UsersAgri.FindAsync(dto.LeaderId);
            if (leader == null)
                return BadRequest("Invalid LeaderId");

            var members = dto.MemberIds != null
                ? await _context.UsersAgri.Where(u => dto.MemberIds.Contains(u.Id)).ToListAsync()
                : new List<User>();

            var cooperative = new Cooperative
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                LeaderId = dto.LeaderId,
                Members = members
            };

            _context.Cooperatives.Add(cooperative);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCooperative), new { id = cooperative.Id }, cooperative);
        }


        // PUT: api/Cooperatives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCooperative(int id, Cooperative cooperative)
        {
            if (id != cooperative.Id)
                return BadRequest();

            _context.Entry(cooperative).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CooperativeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Cooperatives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCooperative(int id)
        {
            var cooperative = await _context.Cooperatives.FindAsync(id);
            if (cooperative == null)
                return NotFound();

            _context.Cooperatives.Remove(cooperative);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CooperativeExists(int id)
        {
            return _context.Cooperatives.Any(c => c.Id == id);
        }
    }
}
