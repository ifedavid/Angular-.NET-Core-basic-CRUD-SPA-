using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DailySpendingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailySpendingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DailySpendings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailySpendings>>> GetSpendings()
        {
            return await _context.Spendings.ToListAsync();
        }

        // GET: api/DailySpendings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailySpendings>> GetDailySpendings(DateTime id)
        {
            var dailySpendings = await _context.Spendings.FindAsync(id);

            if (dailySpendings == null)
            {
                return NotFound();
            }

            return dailySpendings;
        }

        // PUT: api/DailySpendings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailySpendings(DateTime id, DailySpendings dailySpendings)
        {
            if (id != dailySpendings.Date)
            {
                return BadRequest();
            }

            _context.Entry(dailySpendings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailySpendingsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DailySpendings
        [HttpPost]
        public async Task<ActionResult<DailySpendings>> PostDailySpendings(DailySpendings dailySpendings)
        {
            _context.Spendings.Add(dailySpendings);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DailySpendingsExists(dailySpendings.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDailySpendings", new { id = dailySpendings.Date }, dailySpendings);
        }

        // DELETE: api/DailySpendings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DailySpendings>> DeleteDailySpendings(DateTime id)
        {
            var dailySpendings = await _context.Spendings.FindAsync(id);
            if (dailySpendings == null)
            {
                return NotFound();
            }

            _context.Spendings.Remove(dailySpendings);
            await _context.SaveChangesAsync();

            return dailySpendings;
        }

        private bool DailySpendingsExists(DateTime id)
        {
            return _context.Spendings.Any(e => e.Date == id);
        }
    }
}
