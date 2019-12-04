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

        private WebApplication5.Models.UserData CurrentUser {get; set;}
        public StatsController(ApplicationDbContext _context)
        {

            context = _context;
        }
        // GET: api/Stats
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult> DailyStats([FromUri] int id)
        {
            var currentUser = await context.UserData.FindAsync(id);

            CurrentUser = currentUser;

            var todayDate = DateTime.UtcNow;

            CultureInfo myCI = new CultureInfo("en-UK");
            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var weekNumber = myCal.GetWeekOfYear(todayDate, myCWR, myFirstDOW);

          

            var DailySpendings = await context.Spendings.Where(sp => sp.WeekNumber == weekNumber && sp.isDeleted == false && sp.User.Id == id).OrderBy(sp => sp.Date).ToListAsync();

            foreach (var dailySpending in DailySpendings)
            {
                dailySpending.TotalAmount = dailySpending.GetTotalAmount(context, dailySpending.DateId);
            }

            return Ok(new { DailySpendings });

        }


        // POST: api/Stats
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([Microsoft.AspNetCore.Mvc.FromBody] string value)
        {
        }

        //Get Week: api/Stats/action
        [HttpGet("[action]/{userId}/{weekNumber}")]
        public async Task<IActionResult> GetWeek([FromUri] int userId, int weekNumber)
        {
           
            var DailySpendings = await context.Spendings.Where(sp => sp.WeekNumber == weekNumber && sp.isDeleted == false && sp.User.Id == userId).OrderBy(sp => sp.Date).ToListAsync();

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

    public class StatsData
    {
        public int UserId { get; set; }

        public int WeekNumber { get; set; }
    }
}
