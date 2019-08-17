using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    public class TourPlannersController : Controller
    {
        private readonly ApplicationDbContext _db;
        


        public TourPlannersController(ApplicationDbContext db)
        {
            _db = db;
        }

       [HttpGet("[action]")]
       [Authorize(Policy ="IsLoggedIn")]
       public IActionResult GetTourPlanners()
        {
            return Ok(_db.TourPlanners.ToList());
        }


        [HttpPost("[action]")]
        [Authorize(Policy = "IsaPlanner")]
        public async Task<IActionResult> AddTours([FromBody] TourPlannersModel formdata)
        {

            
            var newTourPlanner = new TourPlannersModel
            {
                TourPlannerFullname = formdata.TourPlannerFullname,
                TourPlannerEmail = formdata.TourPlannerEmail,
                TourPlannerPhoneNumber = formdata.TourPlannerPhoneNumber,
                TourPlannerPassword = formdata.TourPlannerPassword,
                AboutTourPlanner = formdata.AboutTourPlanner,
                TourPlannerWebsite = formdata.TourPlannerWebsite
              
            };

            await _db.TourPlanners.AddAsync(newTourPlanner);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateTour([FromRoute] int Id, [FromBody] TourPlannersModel formdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var findTourPlanner = _db.TourPlanners.FirstOrDefault(t => t.TourPlannerId == Id);

            if (findTourPlanner == null)
            {
                return NotFound();
            }

            findTourPlanner.TourPlannerFullname = formdata.TourPlannerFullname;
            findTourPlanner.TourPlannerEmail = formdata.TourPlannerEmail;
            findTourPlanner.TourPlannerPhoneNumber = formdata.TourPlannerPhoneNumber;
            findTourPlanner.TourPlannerPassword = formdata.TourPlannerPassword;
            findTourPlanner.AboutTourPlanner = formdata.AboutTourPlanner;
            findTourPlanner.TourPlannerWebsite = formdata.TourPlannerWebsite;

            _db.Entry(findTourPlanner).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(new { message = "Update successful" });
        }


        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteTour([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var findTourPlanner = await _db.TourPlanners.FindAsync(Id);

            if (findTourPlanner == null)
            {
                return NotFound();
            }

            _db.Remove(findTourPlanner);

            await _db.SaveChangesAsync();

            return Ok(new { Message = "The tour was deleted successfully" });

        }
    }
}
