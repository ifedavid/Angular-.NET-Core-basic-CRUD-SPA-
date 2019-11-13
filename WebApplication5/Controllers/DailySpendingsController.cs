using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;


namespace WebApplication5.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DailySpendingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public DailySpendingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DailySpendings/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{userId}")]
        public async Task<ActionResult> GetSpendings([FromUri] int userId)
        {
            var user = await _context.UserData.FindAsync(userId);

            var dailySpendings =  _context.Spendings.Where(sp => sp.User == user ).ToList();

            if (dailySpendings == null)
            {
                return BadRequest();

            }

            foreach (var dailySpending in dailySpendings)
            {
                dailySpending.TotalAmount = dailySpending.GetTotalAmount(_context, dailySpending.DateId);
                
            }

          

            return Ok( dailySpendings );
        }

  

        // POST: api/DailySpendings
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> PostDailySpendings(DailySpendingModel Spending)
        {
            if (ModelState.IsValid)
            {

                var savedSpending = _context.Spendings.Where(sp => sp.Date == Spending.date);

                if (savedSpending.Count() != 0)
                {
                    return BadRequest(new { message = "Expense tracker for this day exists already" });
                }

                var CurrentUser = _context.UserData.Find(Spending.UserId);

                if (CurrentUser != null && Spending.date != null)
                {

                    var dailySpendings = new DailySpendings
                    {
                        User = CurrentUser,
                        Date = Spending.date,
                        CreatedAt = DateTime.UtcNow.ToLongDateString(),
                        UpdatedAt = DateTime.UtcNow.ToLongDateString()
                    };

                   await _context.Spendings.AddAsync(dailySpendings);

                    await _context.SaveChangesAsync();

                    return Ok(new {dateId = dailySpendings.DateId, date = dailySpendings.Date, message = "Save successful" });

                }

                return BadRequest(new { message = "Invalid user Input" });
            }

            return BadRequest();
        }

        //Update
        [Microsoft.AspNetCore.Mvc.HttpPut]
        public async Task<IActionResult> UpdateDailySpendings(DailySpendingModel spendingModel)
        {
            if (ModelState.IsValid)
            {
                var spending = await _context.Spendings.FindAsync(spendingModel.Id);

                if (spending == null)
                {
                    return BadRequest(new { message = "Spending not found" });
                }

                spending.UpdatedAt = spendingModel.updatedAt;

                _context.Entry(spending).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Update successfully" });
            }

            return BadRequest(new { message = "Sorry, an error occured somewhere here" } );
        }

        // DELETE: api/DailySpendings/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
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

       
    }

    public class DailySpendingModel
    {
        public Guid Id { get; set; }
        public string updatedAt { get; set; }
        public string date { get; set; }
        public int UserId { get; set; }
    }
}
