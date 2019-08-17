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
    public class ToursController : Controller
    {

      private readonly ApplicationDbContext _db;
      public ToursController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("[action]")]
        [Authorize(Policy ="IsLoggedIn")]
        public IActionResult GetTours()
        {
            return Ok(_db.Tours.ToList());
        }

        [HttpPost("[action] ")]
        [Authorize(Policy ="IsaPlanner")]
        public async Task<IActionResult> AddTours([FromBody] ToursModel formdata)
        {
            var newTour = new ToursModel
            {
                TourName = formdata.TourName,
                Description = formdata.Description,
                StartDate = formdata.StartDate,
                EndDate = formdata.EndDate,
                Duration = formdata.Duration,
                OptinPrice = formdata.OptinPrice,
                TourPlanner = formdata.TourPlanner
            };

          var save =  await _db.Tours.AddAsync(newTour);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "IsaPlanner")]
        public async Task<IActionResult> UpdateTour([FromRoute] int Id, [FromBody] ToursModel formdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var findTour = _db.Tours.FirstOrDefault(t => t.TourId == Id);

            if (findTour == null)
            {
                return NotFound();
            }

            findTour.TourName = formdata.TourName;
            findTour.Description = formdata.Description;
            findTour.StartDate = formdata.StartDate;
            findTour.EndDate = formdata.EndDate;
            findTour.Duration = formdata.Duration;
            findTour.OptinPrice = formdata.OptinPrice;

            _db.Entry(findTour).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(new { message = "Update successful" });
        }


        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "IsaPlanner")]
        public async Task<IActionResult> DeleteTour([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var findTour = await _db.Tours.FindAsync(Id);

            if (findTour == null)
            {
                return NotFound();
            }

            _db.Remove(findTour);

           await _db.SaveChangesAsync();

            return Ok(new { Message = "The tour was deleted successfully" });

        }
    }
}
