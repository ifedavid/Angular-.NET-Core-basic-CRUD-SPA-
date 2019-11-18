using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace WebApplication5.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class StatsController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public StatsController(ApplicationDbContext _context)
        {
            context = _context;
        }
        // GET: api/Stats
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult> DailyStats()
        {
            var todayDate = DateTime.UtcNow;

            CultureInfo myCI = new CultureInfo("en-UK");
            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var weekNumber = myCal.GetWeekOfYear(todayDate, myCWR, myFirstDOW);

            var DailySpendings = await context.Spendings.Where(sp => sp.WeekNumber == weekNumber && sp.isDeleted == false).ToListAsync();

            foreach (var dailySpending in DailySpendings)
            {
                dailySpending.TotalAmount = dailySpending.GetTotalAmount(context, dailySpending.DateId);
            }

            return Ok(new { DailySpendings });

        }

        // GET: api/Stats/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stats
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([Microsoft.AspNetCore.Mvc.FromBody] string value)
        {
        }

        // PUT: api/Stats/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public void Put(int id, [Microsoft.AspNetCore.Mvc.FromBody] string value)
        {
        }

        // DELETE: api/Stats/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //Get Week: api/Stats/action
        [HttpGet("[action]/{weekNumber}")]
        public async Task<IActionResult> GetWeek([FromUri] int weekNumber)
        {
           
            var DailySpendings = await context.Spendings.Where(sp => sp.WeekNumber == weekNumber && sp.isDeleted == false).ToListAsync();

            if (DailySpendings.Count > 0)
            {

                foreach (var dailySpending in DailySpendings)
                {
                    dailySpending.TotalAmount = dailySpending.GetTotalAmount(context, dailySpending.DateId);
                }

                return Ok(DailySpendings);
            }

            return BadRequest(new { error = "There are no records for this week" });
        }

    }
}
