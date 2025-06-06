using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApI_1.Data;
using WebApI_1.Models;

namespace WebApI_1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShiftController(AppDbContext context)
        {
            _context = context;
        }

        //Här Är Koden För Att Stämpla in
        [HttpPost("start")]
        public async Task<IActionResult> StartShift()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var activeShift = await _context.Shifts
                .FirstOrDefaultAsync(s => s.UserId == userId && s.EndTime == null);

            if (activeShift != null)
                return BadRequest("Du har redan ett aktivt pass!");

            var shift = new Shift
            {
                StartTime = DateTime.Now,
                UserId = userId!
            };

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(shift);
        }

        // Här Är Koden Som Gör Så Att Man Stämpla ut
        [HttpPost("end")]
        public async Task<IActionResult> EndShift()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var activeShift = await _context.Shifts
                .FirstOrDefaultAsync(s => s.UserId == userId && s.EndTime == null);

            if (activeShift == null)
                return NotFound("Inget aktivt pass att avsluta.");

            activeShift.EndTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(activeShift);
        }

        //Här är koden för att Hämta alla pass
        [HttpGet]
        public async Task<IActionResult> GetMyShifts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shifts = await _context.Shifts
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();

            return Ok(shifts);
        }

        // Här Är Koden För Att Hämta En specifikt Pass
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        //Koden För Att Radera En pass
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (shift == null)
                return NotFound();

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return Ok("Borttaget!");
        }
    }
}
